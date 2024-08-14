using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class UserRepository(FreshCodeContext dbContext) : IUserRepository
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
            var tasks = await _dbContext.Tasks
                .Include(t => t.UserTasks.Where(ut => ut.UserId == user.Id))
                .Select(task=>TaskMapper.ToDTO(task))
                .ToListAsync();
            return tasks;
        }

        public async Task<List<ArtifactHistoryDTO>> GetArtifactHistory(string vk_user_id)
        {
            User user = await _dbContext.Users.FirstAsync(u => u.VkId == Convert.ToInt32(vk_user_id));

            var artifactHistory = await _dbContext.Artifacts
                .Include(a=>a.ArtifatcType)
                .Include(a=>a.Rarity)
                .Include(a=>a.ArtifactBonuses)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Characteristic)
                .Include(a => a.ArtifactBonuses)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Type)
                .Include(a => a.ArtifactHistories.Where(ah => ah.UserId == user.Id))
                .Select(artifact=> ArtifactHistoryMapper.ToDTO(artifact))
                .ToListAsync();
            return artifactHistory;
        }
    }
}
