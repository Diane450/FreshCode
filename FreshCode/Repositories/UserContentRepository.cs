using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class UserContentRepository(FreshCodeContext dbContext) : IUserRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async Task<UserDTO?> GetUserGameInfo(string vk_user_id)
        {
            return await _dbContext.Users
                .Where(u => u.VkId == Convert.ToInt32(vk_user_id))
                .Include(u => u.Background)
                .Select(user => UserMapper.ToDTO(user))
                .FirstOrDefaultAsync();
        }

        public async System.Threading.Tasks.Task InventoryDecreaseFoodCount(string vk_user_id, FoodDTO food)
        {
            User user = await _dbContext.Users.FirstAsync(u => u.VkId == Convert.ToInt32(vk_user_id));
            user.UserFoods.First(uf => uf.Food.Id == food.Id).Count--;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TaskDTO>> GetUserTasks(string vk_user_id)
        {
            User user = await _dbContext.Users.FirstAsync(u => u.VkId == Convert.ToInt32(vk_user_id));
            var tasks = await _dbContext.UserTasks
                .Include(ut=>ut.Task)
                .Select(task => TaskMapper.ToDTO(task))
                .ToListAsync();
            return tasks;
        }

        public async Task<List<ArtifactHistoryDTO>> GetArtifactHistory(string vk_user_id)
        {
            User user = await _dbContext.Users.FirstAsync(u => u.VkId == Convert.ToInt32(vk_user_id));

            return await _dbContext.ArtifactHistories
                .Where(ah=>ah.UserId == user.Id)
                .Include(ah=>ah.Artifact)
                .ThenInclude(a => a.ArtifatcType)
                .Include(ah => ah.Artifact)
                .ThenInclude(a => a.Rarity)
                .Include(ah => ah.Artifact)
                .ThenInclude(a => a.ArtifactBonuses)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Characteristic)
                .Include(ah => ah.Artifact)
                .ThenInclude(a => a.ArtifactBonuses)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Type)
                .Select(artifact => ArtifactHistoryMapper.ToDTO(artifact))
                .ToListAsync();
        }

        public async Task<List<UserFoodDTO>> GetUserFood(string? vk_user_id)
        {
            User user = await _dbContext.Users.FirstAsync(u => u.VkId == Convert.ToInt32(vk_user_id));

            return await _dbContext.UserFoods
                .Where(uf => uf.UserId == user.Id)
                .Include(uf=>uf.Food)
                .ThenInclude(f => f.FoodBonuses)
                .ThenInclude(fb => fb.Bonus)
                .ThenInclude(b => b.Characteristic)
                .Include(uf => uf.Food)
                .ThenInclude(f => f.FoodBonuses)
                .ThenInclude(fb => fb.Bonus)
                .ThenInclude(b => b.Type)
                .Select(f => UserFoodMapper.ToDTO(f))
                .ToListAsync();
        }

        public async Task<List<ArtifactDTO>> GetUserArtifact(string? vk_user_id)
        {
            User user = await _dbContext.Users.FirstAsync(u => u.VkId == Convert.ToInt32(vk_user_id));

            return await _dbContext.UserArtifacts
                .Where(ua => ua.UserId == user.Id)
                .Include(ah => ah.Artifact)
                .ThenInclude(a => a.ArtifatcType)
                .Include(ah => ah.Artifact)
                .ThenInclude(a => a.Rarity)
                .Include(ah => ah.Artifact)
                .ThenInclude(a => a.ArtifactBonuses)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Characteristic)
                .Include(ah => ah.Artifact)
                .ThenInclude(a => a.ArtifactBonuses)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Type)
                .Select(artifactHistory=>ArtifactMapper.ToDTO(artifactHistory.Artifact))
                .ToListAsync();
        }
    }
}
