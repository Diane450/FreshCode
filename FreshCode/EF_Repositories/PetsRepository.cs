using FreshCode.DbModels;
using FreshCode.EF_Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.EF_Repositories
{
    public class PetsRepository(FreshCodeContext dbContext) : IPetsRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async Task<Pet> GetPetByUserId(long userId)
        {
            try
            {
                return await _dbContext.Pets.Where(p=>p.UserId==userId)
                    .Include(p=>p.Level)
                    .Include(p => p.Accessory)
                    .ThenInclude(a => a.Rarity)
                    .Include(p => p.Accessory)
                    .ThenInclude(a => a.ArtifatcType)
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
                    .ThenInclude(a => a.ArtifatcType)
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
                    .ThenInclude(a => a.ArtifatcType)
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
                    .ThenInclude(a => a.ArtifatcType)
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

        public async System.Threading.Tasks.Task ChangePetsArtifact(PetDTO petDTO)
        {
            Pet pet = await _dbContext.Pets.FindAsync(petDTO.Id);
            pet.AccessoryId = petDTO.Accessory.Id;
            pet.HatId = petDTO.Hat.Id;

            pet.CurrentCriticalChance = petDTO.CurrentCriticalChance;
            pet.CurrentDefence = petDTO.CurrentDefence;
            pet.CurrentCriticalDamage = petDTO.CurrentCriticalDamage;
            pet.CurrentHealth = petDTO.CurrentHealth;
            pet.CurrentStrength = petDTO.CurrentStrength;
            //TODO: Изменить на формулу
            pet.AveragePower = petDTO.AveragePower;
            //await _dbContext.SaveChangesAsync();
        }

        public async Task<Pet> CreatePetAsync(CreatePetRequest request, long userId)
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
            await _dbContext.Pets.AddAsync(pet);
            await _dbContext.SaveChangesAsync();
            return pet;
        }

        public async System.Threading.Tasks.Task SaveShangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void UpdateAsync(Pet pet)
        {
            _dbContext.Pets.Update(pet);
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
    }
}
