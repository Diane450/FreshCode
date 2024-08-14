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
    }
}
