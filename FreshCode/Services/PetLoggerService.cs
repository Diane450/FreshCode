using FreshCode.DbModels;
using FreshCode.Interfaces;

namespace FreshCode.Services
{
    public class PetLoggerService(IBaseRepository baseRepository) : IPetLoggerService
    {
        private readonly IBaseRepository _baseRepository = baseRepository;

        public async System.Threading.Tasks.Task CreateFeedLog(Pet pet)
        {
            PetFeedLog petFeedLog = new PetFeedLog()
            {
                PetId = pet.Id,
                CreatedAt = DateTime.UtcNow,
            };
            await _baseRepository.AddAsync(petFeedLog);
            await _baseRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task CreateSleepLog(Pet pet)
        {
            PetSleepLog petSleepLog = new PetSleepLog()
            {
                PetId = pet.Id,
                CreatedAt = DateTime.UtcNow,
                WokeUpAt = DateTime.UtcNow,
            };
            await _baseRepository.AddAsync(petSleepLog);
            await _baseRepository.SaveChangesAsync();
        }
    }
}
