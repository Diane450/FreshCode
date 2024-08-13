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

        public async Task<Pet> CreatePetAsync(CreatePetRequest request, string? vk_user_id)
        {
            //TODO: вот это говно 
            long userId = await _dbContext.Users
                .Where(u => u.VkId == Convert.ToInt32(vk_user_id))
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
            Pet pet = new()
            {
                Name = request.Name,
                UserId = userId,
                Eyes = request.Eyes,
                Body = request.Body,
                SleepNeed = 100,
                FeedNeed = 100,
                FightNeed = 100,
                GeneralHappiness = 100,
                Level = 1,
                Points = 0,
                CurrentHealth = 0,
                CurrentStrength = 0,
                CurrentDefence = 0,
                CurrentCriticalDamage = 0,
                CurrentCriticalChance = 0,
                MaxHealth = 0,
                MaxStrength = 0,
                MaxDefence = 0,
                MaxCriticalDamage = 0,
                MaxCriticalChance = 0,
                AveragePower = 0
            };
            //await _dbContext.Pets.AddAsync(pet);
            //await _dbContext.SaveChangesAsync();
            return pet;
        }

        public async Task<PetDTO> GetPetInfoAsync(int VkId)
        {
            try
            {
                return await _dbContext.Pets.Include(p => p.User)
                    .Where(p => p.User.VkId == VkId)
                    .Include(p => p.Accessory)
                    .Include(p => p.Hat)
                    .Include(p => p.Body)
                    .Include(p => p.Eyes)
                    .Select(p => PetMapper.ToDto(p))
                    .FirstAsync();
            }
            catch (Exception)
            {
                throw new Exception("User has no pet");
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

        //public async Task<Pet> Eat(EatRequest request)
        //{

        //}
    }
}
