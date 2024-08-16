using FreshCode.DbModels;
using FreshCode.Exceptions;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;

namespace FreshCode.UseCases
{
    public class PurchaseUseCase
    {
        private readonly IUserRepository _userRepository;

        public PurchaseUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async System.Threading.Tasks.Task BuyArtifact(ArtifactDTO artifactToBuy, string vk_user_id)
        {
            User user = await _userRepository.GetUserByVkId(vk_user_id);

            user.Money -= artifactToBuy.Price;

            HasPositiveBalance(user);

            user.UserArtifacts.Add(new UserArtifact
            {
                UserId = user.Id,
                ArtifactId = artifactToBuy.Id,
            });

            await _userRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task BuyFood(FoodDTO foodToBuy, string vk_user_id)
        {
            User user = await _userRepository.GetUserByVkId(vk_user_id);

            user.Money -= foodToBuy.Price;
            HasPositiveBalance(user);

            var userFoodList = await _userRepository.GetUserFood(user.Id);
            var userFood = user.UserFoods.First(uf => uf.Food.Id == foodToBuy.Id);
            if (userFood is not null)
            {
                userFood.Count += 1;
            }
            else
            {
                user.UserFoods.Add(new UserFood
                {
                    UserId = user.Id,
                    FoodId = foodToBuy.Id,
                    Count = 1
                });
            }
            await _userRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task BuyBackground(BackgroundDTO backgroundToBuy, string vk_user_id)
        {
            User user = await _userRepository.GetUserByVkId(vk_user_id);

            user.Money -= backgroundToBuy.Price;
            HasPositiveBalance(user);

            user.UserBackgrounds.Add(new UserBackground
            {
                UserId = user.Id,
                BackgroundId = backgroundToBuy.Id,
            });

            await _userRepository.SaveChangesAsync();
        }

        private void HasPositiveBalance(User user)
        {
            if (user.Money < 0)
            {
                throw new InsufficientFundsException();
            }
        }
    }
}
