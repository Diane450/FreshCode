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
            DateTime? userLastWheelRollTime = _fortuneRepository.GetUserLastWheelRollTime(userId);

            Pet pet = await _petRepository.GetPetByUserId(userId);

            if (userLastWheelRollTime is null || (DateTime.UtcNow - userLastWheelRollTime.Value).TotalHours >= 24)
            {
                //крутим колесо
                IQueryable<Bonu> bonuses = _bonusRepository.GetAllBonusesAsync();

                FortuneWheelDropResponse response = _bonusDropService.GetRandomBonus(bonuses);

                //активируем бонусы
                _bonusService.SetBonuses(pet, bonuses.ToList());

                //записать в историю
                UserBonuse userBonuse = new UserBonuse()
                {
                    UserId = userId,
                    BonusId = response.BonusId,
                    CreatedAt = response.CreatedAt,
                    ExpiresAt = response.ExpiresAt,
                };
                UserFortuneWheelSpin spin = new() 
                {
                    UserId = userId,
                    CreatedAt = response.CreatedAt
                };

                await _baseRepository.AddAsync(userBonuse);
                await _baseRepository.AddAsync(spin);
                await _baseRepository.SaveChangesAsync();
                return response;
            }
            else
            {
                throw new ArgumentException("User cannot spin the wheel of fortune yet");
            }
        }
    }
}
