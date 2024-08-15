using FreshCode.DbModels;
using FreshCode.Exceptions;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class PurchaseRepository(FreshCodeContext dbContext) : IPurchaseRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async System.Threading.Tasks.Task BuyArtifact(ArtifactDTO artifactToBuy, User user)
        {
            try
            {
                Artifact? artifact = await _dbContext.Artifacts.FindAsync(artifactToBuy.Id);
                
                if (user is null || artifact is null)
                {
                    throw new ArgumentException("Пользователь или артефакты не найдены");
                }

                user.Money -= artifact.Price;

                if (user.Money < 0)
                {
                    throw new InsufficientFundsException();
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

        public async System.Threading.Tasks.Task BuyFood(FoodDTO foodToBuy, User user)
        {
            try
            {
                Food? food = await _dbContext.Foods.FindAsync(foodToBuy.Id);
                
                if (user is null || food is null)
                {
                    throw new ArgumentException("Пользователь или еда не найдены");
                }

                user.Money -= food.Price;

                if (user.Money < 0)
                {
                    throw new InsufficientFundsException();
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

        public async System.Threading.Tasks.Task BuyBackground(BackgroundDTO backgroundToBuy, User user)
        {
            try
            {
                Background? background = await _dbContext.Backgrounds.FindAsync(backgroundToBuy.Id);
                
                if (user is null || background is null)
                {
                    throw new ArgumentException("Пользователь или фон не найдены");
                }

                user.Money -= background.Price;

                if (user.Money < 0)
                {
                    throw new InsufficientExecutionStackException();
                }

                user.UserBackgrounds.Add(new UserBackground
                {
                    User = user,
                    Background = background
                });

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Не удалось совершить покупку");
            }
        }
    }
}
