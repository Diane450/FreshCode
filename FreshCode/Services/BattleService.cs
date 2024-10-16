using FreshCode.DbModels;
using FreshCode.Hubs;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;
using FreshCode.Responses;
using FreshCode.UseCases;
using Microsoft.AspNetCore.SignalR;

namespace FreshCode.Services
{
    public class BattleService
    {
        private readonly IHubContext<BattleHub> _hubContext;
        private readonly IBattleRepository _battleRepository;
        private readonly IPetsRepository _petRepository;
        private readonly PetsUseCase _petsUseCase;
        private readonly UserUseCase _userUseCase;
        private readonly BattleUseCase _battleUseCase;

        public BattleService(IHubContext<BattleHub> hubContext,
            IBattleRepository battleRepository,
            IPetsRepository petRepository,
            PetsUseCase petsUseCase,
            UserUseCase userUseCase,
            BattleUseCase battleUseCase)
        {
            _hubContext = hubContext;
            _battleRepository = battleRepository;
            _petRepository = petRepository;
            _petsUseCase = petsUseCase;
            _userUseCase = userUseCase;
            _battleUseCase = battleUseCase;
        }

        // Метод расчёта урона
        public int CalculateDamage(PetStatResponse attacker, PetStatResponse defender)
        {
            bool isCriticalHit = (decimal)new Random().NextDouble() <= attacker.CriticalChance / 100;

            decimal damage;

            if (isCriticalHit)
            {
                damage = attacker.Strength * (1 + (attacker.CriticalDamage / 100));
            }
            else
            {
                damage = attacker.Strength;
            }
            // Базовый урон

            var damageAbsorption = (decimal)defender.Defence / (attacker.Defence + 1500);

            var baseDamage = damage * (1 - damageAbsorption);

            return Convert.ToInt32(baseDamage);
        }

        // Обработка удара
        public async Task<bool> HandleAttack(BattleDTO battle)
        {
            Pet attackerPet = await _petRepository.GetPetByUserId(Convert.ToInt64(battle.Attacker.UserId));
            Pet defenderPet = await _petRepository.GetPetByUserId(Convert.ToInt64(battle.Defender.UserId));

            PetStatResponse attackerStats = await _petsUseCase.GetPetStats(Convert.ToInt64(attackerPet.Id));
            PetStatResponse defenderStats = await _petsUseCase.GetPetStats(Convert.ToInt64(defenderPet.Id));

            // Рассчитываем урон
            var damage = CalculateDamage(attackerStats, defenderStats);
            defenderPet.CurrentHealth = Math.Max(defenderPet.CurrentHealth - damage, 0); // Обновляем здоровье

            // Получаем кортеж
            var attacker = battle.Attacker;

            // Изменяем поле Movecount
            attacker.Movecount -= 1;

            // Присваиваем обновленный кортеж обратно
            battle.Attacker = attacker;

            var message = new
            {
                attacker_damage = damage,
                defender_health = defenderPet.CurrentHealth
            };
            // Уведомляем обоих игроков о результате удара
            await _hubContext.Clients.Group(battle.BattleId.ToString()).SendAsync("ReceiveAttackResult", damage, message);

            await _hubContext.Clients.Client(battle.Attacker.ConnectionId).SendAsync("InformAttackerMoveCount", $"Ходов осталось: {battle.Attacker.Movecount}");

            // Проверяем конец боя
            if (defenderPet.CurrentHealth <= 0)
            {
                await _hubContext.Clients.Group(battle.BattleId.ToString()).SendAsync("BattleEnded", battle.Attacker.UserId); // Уведомляем о завершении боя
                //логика для удаления пользователей сражения из списка
            }

            if (battle.Attacker.Movecount == 0 || defenderPet.CurrentHealth <= 0)
            {
                (string ConnectionId, long UserId, PetDTO pet, long vk_user_id, int Movecount) winner;
                (string ConnectionId, long UserId, PetDTO pet, long vk_user_id, int Movecount) loser;
                
                if (battle.Attacker.pet.CurrentHealth != battle.Defender.pet.CurrentHealth)
                {
                    winner = battle.Attacker.pet.CurrentHealth > battle.Defender.pet.CurrentHealth ? battle.Attacker : battle.Defender;
                    loser = battle.Attacker.pet.CurrentHealth > battle.Defender.pet.CurrentHealth ? battle.Defender : battle.Attacker;
                }
                else
                {
                    winner = battle.Attacker.Movecount > battle.Defender.Movecount ? battle.Attacker : battle.Defender;
                    loser = battle.Attacker.Movecount > battle.Defender.Movecount ? battle.Defender : battle.Attacker;
                }

                await _hubContext.Clients.Group(battle.BattleId.ToString()).SendAsync("BattleEnded", winner.vk_user_id); // Уведомляем о завершении боя

                var reward = new RewardResponse
                {
                    Money = CalculateReward(100, winner.pet.Level),
                    Points = CalculateReward(100, winner.pet.Level),
                    StatPoints = CalculateReward(100, winner.pet.Level),
                    Primogems = CalculateReward(100, winner.pet.Level)
                };
                await _hubContext.Clients.Client(winner.ConnectionId).SendAsync("WinnerReward", reward); // Уведомляем о завершении боя

                //await _petsUseCase.AddPoints(reward.points);
                //await _userUseCase.AddReward(reward);
                
                await _battleUseCase.UpdateBattle(battle.BattleId, reward, winner.UserId);
                
                await _hubContext.Clients.Group(battle.BattleId.ToString()).SendAsync("BattleEnded", winner.vk_user_id); // Уведомляем о завершении боя                
                return true;
            }

            var defender = battle.Defender;
            battle.Defender = battle.Attacker;
            battle.Attacker = defender;

            await _hubContext.Clients.Client(battle.Attacker.ConnectionId).SendAsync("InformPlayerTurn", "Ваш ход");
            await _hubContext.Clients.Client(battle.Defender.ConnectionId).SendAsync("InformPlayerTurn", "Ход противника");
            return false;
        }

        private int CalculateReward(int baseReward, long petLevel)
        {
            double reductionFactor = Math.Pow(0.95, petLevel); // Чем больше уровень, тем сильнее снижение
            return (int)Math.Max(1, baseReward * reductionFactor);
        }

        public async System.Threading.Tasks.Task ProcessBattleEnd(BattleDTO battle)
        {
            var battleId = battle.BattleId.ToString();

            // Уведомляем о завершении боя
            await _hubContext.Clients.Group(battleId).SendAsync("BattleEnded", battle.Attacker.UserId);

            // Удаляем игроков из группы через хаб
            await _hubContext.Clients.Client(battle.Attacker.ConnectionId).SendAsync("BattleEnded", "Вы победили!");
            await _hubContext.Clients.Client(battle.Defender.ConnectionId).SendAsync("BattleEnded", "Вы проиграли!");

            // Вызываем удаление из группы через хаб
            await _hubContext.Clients.Client(battle.Attacker.ConnectionId).SendAsync("RemoveFromGroup", battleId, battle.Attacker.ConnectionId);
            await _hubContext.Clients.Client(battle.Defender.ConnectionId).SendAsync("RemoveFromGroup", battleId, battle.Defender.ConnectionId);
        }
    }
}