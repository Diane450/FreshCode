using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Responses;
using FreshCode.Services;

namespace FreshCode.UseCases
{
    public class FortuneWheelUseCase
    {
        private readonly IFortuneRepository _fortuneRepository;
        private readonly FortuneWheelBonusDropService _bonusDropService;
        private readonly IBonusRepository _bonusRepository;
        private readonly IPetBonusManagerService _bonusService;
        private readonly IPetsRepository _petRepository;

        public FortuneWheelUseCase(IFortuneRepository fortuneRepository,
            FortuneWheelBonusDropService bonusDropService,
            IBonusRepository bonusRepository,
            IPetBonusManagerService bonusService,
            IPetsRepository petRepository)

        {
            _fortuneRepository = fortuneRepository;
            _bonusDropService = bonusDropService;
            _bonusService = bonusService;
            _petRepository = petRepository;
        }
        public async void SpinFortuneWheel(long userId)
        {
            DateTime? userLastWheelRollTime = await _fortuneRepository.GetUserLastWheelRollTime(userId);

            Pet pet = await _petRepository.GetPetByUserId(userId);

            if (userLastWheelRollTime is null || (DateTime.Now - userLastWheelRollTime.Value).TotalHours >= 24)
            {
                //крутим колесо
                IQueryable<Bonu> bonuses = _bonusRepository.GetAllBonusesAsync();

                FortuneWheelDropResponse response = _bonusDropService.GetRandomBonus(bonuses);

                //активируем бонусы
                _bonusService.SetBonuses(pet, bonuses.ToList());

                //записать в историю
                FortuneWheelResult fortuneWheelResult = new FortuneWheelResult()
                {
                    UserId = userId,
                    BonusId = response.BonusId,
                    CreatedAt = response.CreatedAt,
                    ExpiresAt = response.ExpiresAt,
                };
            }
            else
            {
                throw new ArgumentException("User cannot spin the wheel of fortune yet");
            }
            //проверка, давно ли пользователь крутил колесо
            //если да, то выдаем рандом значение
            //если нет, то говорим, что еще рано крутить колесо
        }
    }
}
