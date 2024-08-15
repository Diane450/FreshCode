using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class PurchaseRepository(FreshCodeContext dbContext) : IPurchaseRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async System.Threading.Tasks.Task BuyArtifact(ArtifactDTO artifactToBuy, string vk_user_id)
        {
            try
            {
                User? user = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.VkId == Convert.ToInt32(vk_user_id));

                Artifact? artifact = await _dbContext.Artifacts.FindAsync(artifactToBuy.Id);
                
                user.Money -= artifact.Price;
                
                if (user.Money<0)
                {
                    throw new Exception();
                }

                user.UserArtifacts.Add(new UserArtifact
                {
                    User = user,
                    Artifact = artifact
                });

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Не удалось совершить покупку");
            }
        }

        public async System.Threading.Tasks.Task BuyFood(FoodDTO foodToBuy, string? vk_user_id)
        {
            try
            {
                User? user = await _dbContext.Users
                    .Where(u => u.VkId == Convert.ToInt32(vk_user_id))
                    .Include(u => u.UserFoods)
                    .FirstOrDefaultAsync();

                Food? food = await _dbContext.Foods.FindAsync(foodToBuy.Id);

                user.Money -= food.Price;

                if (user.Money < 0)
                {
                    throw new Exception();
                }

                var userFood = user.UserFoods.FirstOrDefault(uf => uf.FoodId == food.Id);
                if (userFood is not null)
                {
                    userFood.Count += 1;
                }
                else
                {
                    user.UserFoods.Add(new UserFood
                    {
                        User = user,
                        Food = food,
                        Count = 1
                    });
                }
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
