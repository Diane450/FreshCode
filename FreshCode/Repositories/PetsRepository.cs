using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class PetsRepository : IPetsRepository
    {
        private readonly FreshCodeContext _dbContext;

        public PetsRepository(FreshCodeContext dbContext)
        {
            _dbContext = dbContext;
        }

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
                EyesId = request.Eyes_Id,
                BodyId = request.Body_Id,
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

        public async Task<Pet> GetPetInfoAsync(int VkId)
        {
            try
            {
                return await _dbContext.Pets.Include(p => p.User)
                    .Where(p => p.User.VkId == VkId)
                    .Include(p => p.Accessory)
                    .Include(p => p.Hat)
                    .Include(p => p.Body)
                    .Include(p => p.Eyes)
                    .FirstAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("User has no pet");
            }
        }

        public async Task<Pet> LevelUpAsync(Pet pet)
        {
            _dbContext.Attach(pet);
            pet.Level += 1;
            //TODO: прибавление максимальных значений для питомца.
            await _dbContext.SaveChangesAsync();
            return pet;
        }

        //public async Task<Pet> Eat(EatRequest request)
        //{
            
        //}
    }
}
