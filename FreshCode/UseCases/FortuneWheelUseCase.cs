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
                IQueryable<Bonu> bonuses = _bonusRepository.GetAllBonusesAsync();

                Bonu response = _bonusDropService.GetRandomBonus(bonuses);

                FortuneWheelDropResponse fortuneWheelResponse = new()
                {
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddSeconds(response.Duration),
                    BonusFormat = response.Type.Type,
                    Characteristic = response.Characteristic.Characteristic1,
                    Value = response.Value,
                };

                if (response.Characteristic.Characteristic1 == "SleepNeed" || response.Characteristic.Characteristic1 == "FeedNeed")
                {
                    var list = new List<Bonu>()
                    {
                        response
                    };
                    _bonusService.SetBonuses(pet, list);
                }
                else
                {
                    UserBonuse userBonuse = new UserBonuse()
                    {
                        PetId = pet.Id,
                        BonusId = response.Id,
                        CreatedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddSeconds(response.Duration),
                        BonusTypeId = 1
                    };
                    UserFortuneWheelSpin spin = new()
                    {
                        UserId = userId,
                        CreatedAt = userBonuse.CreatedAt
                    };

                    await _baseRepository.AddAsync(userBonuse);
                    await _baseRepository.AddAsync(spin);
                    await _baseRepository.SaveChangesAsync();
                }
                return fortuneWheelResponse;
            }
            else
            {
                throw new ArgumentException("User cannot spin the wheel of fortune yet");
            }
        }
    }
}
