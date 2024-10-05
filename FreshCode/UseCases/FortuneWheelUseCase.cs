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


        public FortuneWheelUseCase(IFortuneRepository fortuneRepository,
            FortuneWheelBonusDropService bonusDropService,
            IBonusRepository _bonusRepository
            )
        {
            _fortuneRepository = fortuneRepository;
            _bonusDropService = bonusDropService;
        }
        public async void SpinFortuneWheel(long userId)
        {
            DateTime? userLastWheelRollTime = await _fortuneRepository.GetUserLastWheelRollTime();

            if (userLastWheelRollTime is null || (DateTime.Now - userLastWheelRollTime.Value).TotalHours >= 24)
            {
                //крутим колесо
                IQueryable<Bonu> bonuses = _bonusRepository.GetAllBonusesAsync();

                FortuneWheelDropResponse response = _bonusDropService.GetRandomBonus(bonuses);
                
                //активируем бонусы
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
