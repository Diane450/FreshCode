using FreshCode.DbModels;
using FreshCode.Responses;
using FreshCode.Services;

namespace FreshCode.UseCases
{
    public class FortuneWheelUseCase
    {
        private readonly IFortuneRepository _fortuneRepository;
        private readonly FortuneWheelBonusDropService _bonusDropService;

        public FortuneWheelUseCase(IFortuneRepository fortuneRepository, FortuneWheelBonusDropService bonusDropService)
        {
            _fortuneRepository = fortuneRepository;
            _bonusDropService = bonusDropService;
        }
        public async void SpinFortuneWheel(long userId)
        {
            DateTime userLastWheelRollTime = await _fortuneRepository.GetUserLastWheelRollTimeAsync();

            if ((DateTime.Now - userLastWheelRollTime).TotalHours >= 24)
            {
                //крутим колесо
                IQueryable<Bonu> bonuses = await _bonusRepository.GetAllBonusesAsync();

                FortuneWheelDropResponse response = _bonusDropService.GetRandomBonus(bonuses);
                
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
