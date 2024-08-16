using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class PetsRepository(FreshCodeContext dbContext) : IPetsRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async Task<PetDTO> GetPetByUserId(int VkId)
        {
            try
            {
                return await _dbContext.Pets.Include(p => p.User)
                    .Where(p => p.User.VkId == VkId)
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
                    .Select(p => PetMapper.ToDto(p))
                    .FirstAsync();
            }
            catch (Exception)
            {
                throw new Exception("User has no petDTO");
            }
        }

        public async Task<PetDTO> LevelUpAsync(PetDTO petDto)
        {
            Pet? pet = await _dbContext.Pets.Where(p => p.Id == petDto.Id)
                .Include(p => p.Accessory)
                .Include(p => p.Hat)
                .Include(p => p.Body)
                .Include(p => p.Eyes)
                .FirstAsync();

            Level? nextLevel = await _dbContext.Levels.FindAsync(petDto.Level + 1);
            if (pet == null)
            {
                throw new Exception("Pet not found");
            }
            if (nextLevel == null)
            {
                throw new Exception("Max level reached");
            }
            pet.Level += 1;
            pet.MaxHealth = nextLevel.MaxHealth;
            pet.MaxStrength = nextLevel.MaxStrength;
            pet.MaxDefence = nextLevel.MaxDefence;
            pet.MaxCriticalDamage = nextLevel.MaxCriticalDamage;
            pet.MaxCriticalChance = nextLevel.MaxCriticalChance;

            PetDTO newPetDTO = PetMapper.ToDto(pet);
            //TODO: прибавление максимальных значений для питомца.
            //await _dbContext.SaveChangesAsync();
            return newPetDTO;
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

        public async Task<Pet> GetPetById(long id)
        {
            return await _dbContext.Pets.FindAsync(id);
        }

        public async System.Threading.Tasks.Task SaveShangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
