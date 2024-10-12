using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Requests;
using FreshCode.Responses;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class PetsRepository(FreshCodeContext dbContext) : IPetsRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async Task<Pet> GetPetByUserId(long userId)
        {
            try
            {
                return await _dbContext.Pets.Where(p => p.UserId == userId)
                    .Include(p => p.Level)
                    .Include(p => p.Accessory)
                    .Include(p => p.Hat)
                    .Include(p => p.Body)
                    .Include(p => p.Eyes)
                    .FirstAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException("У пользователя нет питомца");
            }
        }

        public async Task<Pet> GetPetById(long petId)
        {
            try
            {
                return await _dbContext.Pets.Where(p => p.Id == petId)
                    .Include(p => p.Level)
                    .Include(p => p.Accessory)
                    .ThenInclude(a => a.Rarity)
                    .Include(p => p.Accessory)
                    .ThenInclude(a => a.ArtifactType)
                    .Include(p => p.Accessory)
                    .ThenInclude(a => a.ArtifactBonuses)
                    .ThenInclude(ab => ab.Bonus)
                    .ThenInclude(b => b.Characteristic)
                    .Include(p => p.Accessory)
                    .ThenInclude(a => a.ArtifactBonuses)
                    .ThenInclude(ab => ab.Bonus)
                    .ThenInclude(b => b.Type)

                    .Include(p => p.Hat)
                    .ThenInclude(a => a.Rarity)
                    .Include(p => p.Hat)
                    .ThenInclude(a => a.ArtifactType)
                    .Include(p => p.Hat)
                    .ThenInclude(a => a.ArtifactBonuses)
                    .ThenInclude(ab => ab.Bonus)
                    .ThenInclude(b => b.Characteristic)
                    .Include(p => p.Hat)
                    .ThenInclude(a => a.ArtifactBonuses)
                    .ThenInclude(ab => ab.Bonus)
                    .ThenInclude(b => b.Type)

                    .Include(p => p.Body)
                    .Include(p => p.Eyes)
                    .FirstAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException("Питомца не существует");
            }
        }

        public Pet CreatePet(CreatePetRequest request, long userId)
        {
            Pet pet = new()
            {
                Name = request.Name,
                UserId = userId,
                EyesId = request.Eyes.Id,
                BodyId = request.Body.Id,
                SleepNeed = 100,
                FeedNeed = 100,
                FightNeed = 100,
                GeneralHappiness = 100,
                LevelId = 2,
                Points = 0,
                CurrentHealth = 0,
                CurrentStrength = 0,
                CurrentDefence = 0,
                CurrentCriticalDamage = 0,
                CurrentCriticalChance = 0,
                AveragePower = 0
            };
            return pet;
        }

        public async Task<Level> GelLevelValues(long levelId)
        {
            var level = await _dbContext.Levels.FindAsync(levelId);

            if (level == null)
            {
                throw new ArgumentException("Пользователь достиг максимального уровня");

            }
            return level;
        }

        public IQueryable<UserBonuse> GetPetBonuses(long petId)
        {
            return _dbContext.UserBonuses
                .Where(ub => ub.PetId == petId
                && ub.ExpiresAt > DateTime.UtcNow)
                .Include(b => b.Bonus)
                .ThenInclude(b => b.Characteristic)
                .Include(b => b.Bonus)
                .ThenInclude(b => b.Type);
        }

        public async Task<PetStatResponse> GetPetStats(long petId)
        {
            return await _dbContext.Pets
                .Where(p => p.Id == petId)
                .Select(p => new PetStatResponse
                {
                    CriticalChance = p.CurrentCriticalChance,
                    CriticalDamage = p.CurrentCriticalDamage,
                    Health = p.CurrentHealth,
                    Strength = p.CurrentStrength,
                    Defence = p.CurrentDefence,
                    SleepNeed = p.SleepNeed,
                    FeedNeed = p.FeedNeed,
                }).FirstAsync();
        }

        public IQueryable<PetFeedLog> GetFeedPetLogLast5Minute(long petId)
        {
            return _dbContext.PetFeedLogs
                .Where(pfl => pfl.PetId == petId && pfl.CreatedAt > DateTime.UtcNow.AddMinutes(-5));
        }

    }
}
