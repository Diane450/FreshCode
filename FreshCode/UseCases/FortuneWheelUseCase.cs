using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Responses;
using FreshCode.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.UseCases
{
    public class FortuneWheelUseCase
    {
        private readonly IFortuneRepository _fortuneRepository;
        private readonly FortuneWheelBonusDropService _bonusDropService;
        private readonly IBonusRepository _bonusRepository;
        private readonly IPetBonusManagerService _bonusService;
        private readonly IPetsRepository _petRepository;
        private readonly IBaseRepository _baseRepository;

        public FortuneWheelUseCase(IFortuneRepository fortuneRepository,
            FortuneWheelBonusDropService bonusDropService,
            IBonusRepository bonusRepository,
            IPetBonusManagerService bonusService,
            IPetsRepository petRepository,
            IBaseRepository baseRepository)
        {
            _fortuneRepository = fortuneRepository;
            _bonusDropService = bonusDropService;
            _bonusService = bonusService;
            _petRepository = petRepository;
            _baseRepository = baseRepository;
            _bonusRepository = bonusRepository;
        }
        public async Task<FortuneWheelDropResponse> SpinFortuneWheel(long userId)
        {
            // Получаем время последнего кручения колеса
            DateTime? lastSpinTime = _fortuneRepository.GetUserLastWheelRollTime(userId);

            // Рассчитываем время, когда можно будет снова крутить колесо
            DateTime nextSpinAvailableTime = lastSpinTime?.AddHours(24) ?? DateTime.UtcNow;

            // Проверка: если пользователь не крутил колесо или прошло 24 часа
            if (DateTime.UtcNow < nextSpinAvailableTime)
            {
                TimeSpan timeUntilNextSpin = nextSpinAvailableTime - DateTime.UtcNow;
                throw new ArgumentException($"Вы сможете крутить колесо через {timeUntilNextSpin.TotalHours:F0} часов.");
            }

            // Получаем питомца пользователя
            Pet pet = await _petRepository.GetPetByUserId(userId);

            // Получаем все доступные бонусы
            IQueryable<Bonu> bonuses = _bonusRepository.GetAllBonusesAsync();

            // Выбираем случайный бонус
            Bonu selectedBonus = _bonusDropService.GetRandomBonus(bonuses);

            // Создаем ответ с информацией о бонусе
            FortuneWheelDropResponse fortuneWheelResponse = new FortuneWheelDropResponse
            {
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddSeconds(selectedBonus.Duration),
                BonusFormat = selectedBonus.Type.Type,
                Characteristic = selectedBonus.Characteristic.Characteristic1,
                Value = selectedBonus.Value,
            };

            // Применяем бонус: для характеристики 4 или 5 применяем к питомцу, иначе создаем бонус для пользователя
            if (selectedBonus.CharacteristicId == 4 || selectedBonus.CharacteristicId == 5)
            {
                await _bonusService.SetBonus(pet, selectedBonus);
            }
            else
            {
                UserBonuse userBonus = new UserBonuse
                {
                    PetId = pet.Id,
                    BonusId = selectedBonus.Id,
                    CreatedAt = fortuneWheelResponse.CreatedAt,
                    ExpiresAt = fortuneWheelResponse.ExpiresAt,
                    BonusTypeId = 1 // тип бонуса
                };

                UserFortuneWheelSpin spinRecord = new UserFortuneWheelSpin
                {
                    UserId = userId,
                    CreatedAt = fortuneWheelResponse.CreatedAt
                };

                // Добавляем запись в базу данных
                await _baseRepository.AddAsync(userBonus);
                await _baseRepository.AddAsync(spinRecord);
                await _baseRepository.SaveChangesAsync();
            }

            return fortuneWheelResponse;
        }

        public async Task<(bool, int?)> IsSpinAvailable(long userId)
        {
            // Получаем время последнего кручения колеса
            DateTime? lastSpinTime = _fortuneRepository.GetUserLastWheelRollTime(userId);

            // Если пользователь никогда не крутил колесо или прошло более 24 часов
            if (lastSpinTime == null || DateTime.UtcNow >= lastSpinTime.Value.AddHours(24))
            {
                return (true, null);
            }

            // Рассчитываем, сколько времени осталось до следующей возможности крутить колесо
            TimeSpan timeUntilNextSpin = lastSpinTime.Value.AddHours(24) - DateTime.UtcNow;

            // Возвращаем информацию о том, через сколько часов можно будет снова крутить (округление до целого)
            return (false, (int)Math.Ceiling(timeUntilNextSpin.TotalHours));
        }
    }
}

