using FreshCode.DbModels;
using FreshCode.Hubs;
using FreshCode.Interfaces;
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
        public decimal CalculateDamage(PetStatResponse attacker, PetStatResponse defender)
        {
            bool isCriticalHit = (decimal)new Random().NextDouble() <= attacker.CriticalChance;

            decimal damage;

            if (isCriticalHit)
            {
                damage = attacker.Strength * (1 + attacker.CriticalDamage);
            }
            else
            {
                damage = attacker.Strength;
            }
            // Базовый урон

            var damageAbsorption = (decimal)defender.Defence / (attacker.Defence + 1500);

            var baseDamage = damage * (1 - damageAbsorption);

            if (isCriticalHit)
            {
                baseDamage = (int)(baseDamage * attacker.CriticalDamage);
            }
            return baseDamage;
        }

        // Обработка удара
        public async System.Threading.Tasks.Task HandleAttack(string battleId, string attackerId, string defenderId)
        {
            // Ищем бой и питомцев
            var battle = _battleRepository.GetBattleById(battleId);

            PetStatResponse attackerStats = await _petsUseCase.GetPetStats(Convert.ToInt64(attackerId));
            PetStatResponse defenderStats = await _petsUseCase.GetPetStats(Convert.ToInt64(defenderId));

            //Pet attackerPet = await _petRepository.GetPetByUserId(Convert.ToInt64(attackerId));
            //Pet defenderPet = await _petRepository.GetPetByUserId(Convert.ToInt64(defenderId));

            //var attackerId = battle.Player1Id == attackerId ? battle.Player1Pet : battle.Player2Pet;
            //var defender = battle.Player1Id == defenderId ? battle.Player1Pet : battle.Player2Pet;

            // Рассчитываем урон
            var damage = CalculateDamage(attackerStats, defenderStats);
            defender.CurrentHealth = Math.Max(defender.CurrentHealth - damage, 0); // Обновляем здоровье

            // Уведомляем обоих игроков о результате удара
            await _hubContext.Clients.User(attackerId).SendAsync("ReceiveAttackResult", damage, defender.CurrentHealth);
            await _hubContext.Clients.User(defenderId).SendAsync("ReceiveAttackResult", damage, defender.CurrentHealth);

            // Проверяем конец боя
            if (defender.CurrentHealth <= 0)
            {
                battle.Status = "Completed";
                await _hubContext.Clients.Group(battleId).SendAsync("BattleEnded", attackerId); // Уведомляем о завершении боя
            }
        }
    }
}
