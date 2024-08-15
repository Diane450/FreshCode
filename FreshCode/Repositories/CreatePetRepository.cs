using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class CreatePetRepository : ICreatePetRepository
    {
        private readonly FreshCodeContext _dbContext;

        public CreatePetRepository(FreshCodeContext freshCodeContext)
        {
            _dbContext = freshCodeContext;
        }

        public async Task<List<EyeDTO>> GetEyesAsync()
        {
            return await _dbContext.Eyes
                .Select(eye => EyeMapper.ToDTO(eye))
                .ToListAsync();
        }

        public async Task<List<BodyDTO>> GetBodiesAsync()
        {
            return await _dbContext.Bodies
                .Select(body => BodyMapper.ToDTO(body))
                .ToListAsync();
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
                Eyes = EyeMapper.ToEntity(request.Eyes),
                Body = BodyMapper.ToEntity(request.Body),
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
    }
}
