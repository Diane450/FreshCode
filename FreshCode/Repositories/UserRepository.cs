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

        public async Task<long> GetUserIdByVkId(string vk_user_id)
        {
            try
            {
                return await _dbContext.Users
                    .Where(u => u.VkId == Convert.ToInt32(vk_user_id))
                    .Select(u => u.Id)
                    .FirstAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException("Пользователь не найден");
            }
        }

        public async Task<UserDTO> GetUserGameInfo(long userId)
        {
            return await _dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Background)
                .Select(user => UserMapper.ToDTO(user))
                .FirstAsync();
        }

        public async Task<List<TaskDTO>> GetUserTasks(long userId)
        {
            return await _dbContext.UserTasks
                .Where(ut => ut.UserId == userId)
                .Include(ut => ut.Task)
                .Select(task => TaskMapper.ToDTO(task))
                .ToListAsync();
        }

        public async Task<List<ArtifactHistoryDTO>> GetArtifactHistory(long userId)
        {
            return await _dbContext.ArtifactHistories
                .Where(ah => ah.UserId == userId)
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
                .Select(artifact => ArtifactHistoryMapper.ToDTO(artifact))
                .ToListAsync();
        }

        public async Task<List<UserFoodDTO>> GetUserFood(long userId)
        {
            return await _dbContext.UserFoods
                .Where(uf => uf.UserId == userId)
                .Include(uf => uf.Food)
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

        public async Task<List<ArtifactDTO>> GetUserArtifact(long userId)
        {
            return await _dbContext.UserArtifacts
                .Where(ua => ua.UserId == userId)
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
                .Select(artifactHistory => ArtifactMapper.ToDTO(artifactHistory.Artifact))
                .ToListAsync();
        }

        public async Task<List<BackgroundDTO>> GetUserBackgrounds(long userId)
        {
            return await _dbContext.UserBackgrounds
                .Where(ub => ub.UserId == userId)
                .Include(ub => ub.Background)
                .Select(userBackground => BackgroundMapper.ToDTO(userBackground.Background))
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByVkId(string vk_user_id)
        {
            try
            {
                return await _dbContext.Users
                    .FirstAsync(u => u.VkId == Convert.ToInt32(vk_user_id));
            }
            catch (Exception)
            {
                throw new ArgumentException("Пользователь не найден");
            }
        }
    }
}
