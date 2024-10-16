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

        public BattleService(IHubContext<BattleHub> hubContext,
            IBattleRepository battleRepository,
            IPetsRepository petRepository,
            PetsUseCase petsUseCase)
        {
            _hubContext = hubContext;
            _battleRepository = battleRepository;
            _petRepository = petRepository;
            _petsUseCase = petsUseCase;
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
        public async System.Threading.Tasks.Task HandleAttack(BattleDTO battle)
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
            var defender = battle.Defender;
            battle.Defender = battle.Attacker;
            battle.Attacker = defender;

            await _hubContext.Clients.Client(battle.Attacker.ConnectionId).SendAsync("InformPlayerTurn", "Ваш ход");
            await _hubContext.Clients.Client(battle.Defender.ConnectionId).SendAsync("InformPlayerTurn", "Ход противника");
        }
    }
}