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
        public int CalculateDamage(PetStatResponse attacker, PetStatResponse defender)
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
            return Convert.ToInt32(baseDamage);
        }

        // Обработка удара
        public async System.Threading.Tasks.Task HandleAttack(string battleId, string attackerId, string defenderId)
        {
            PetStatResponse attackerStats = await _petsUseCase.GetPetStats(Convert.ToInt64(attackerId));
            PetStatResponse defenderStats = await _petsUseCase.GetPetStats(Convert.ToInt64(defenderId));

            Pet attackerPet = await _petRepository.GetPetByUserId(Convert.ToInt64(attackerId));
            Pet defenderPet = await _petRepository.GetPetByUserId(Convert.ToInt64(defenderId));

            // Рассчитываем урон
            var damage = CalculateDamage(attackerStats, defenderStats);
            defenderPet.CurrentHealth = Math.Max(defenderPet.CurrentHealth - damage, 0); // Обновляем здоровье

            // Уведомляем обоих игроков о результате удара
            await _hubContext.Clients.User(attackerId).SendAsync("ReceiveAttackResult", damage, defenderPet.CurrentHealth);
            await _hubContext.Clients.User(defenderId).SendAsync("ReceiveAttackResult", damage, defenderPet.CurrentHealth);

            // Проверяем конец боя
            if (defenderPet.CurrentHealth <= 0)
            {
                await _hubContext.Clients.Group(battleId).SendAsync("BattleEnded", attackerId); // Уведомляем о завершении боя
            }
        }
    }
}