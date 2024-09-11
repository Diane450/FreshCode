using FreshCode.DbModels;
using FreshCode.Exceptions;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.UseCases
{
    public class PurchaseUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IArtifactRepository _artifactRepository;

        public PurchaseUseCase(IUserRepository userRepository,
            IBaseRepository baseRepository)
            {
                _userRepository = userRepository;
                _baseRepository = baseRepository;
            }
        public async System.Threading.Tasks.Task BuyArtifact(BuyArtifactRequest artifactToBuy, string vk_user_id)
        {
            User user = await _userRepository.GetUserByVkId(vk_user_id);

            if (await _userRepository.isArtifactAbsent(artifactToBuy.ArtifactId, user.Id))
            {
                throw new InvalidOperationException("User already owns this item.");
            }

            user.Money -= artifactToBuy.Price;

            HasPositiveBalance(user);

            user.UserArtifacts.Add(new UserArtifact
            {
                UserId = user.Id,
                ArtifactId = artifactToBuy.ArtifactId,
            });

            await _baseRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task BuyFood(BuyFoodRequest foodToBuy, string vk_user_id)
        {
            User user = await _userRepository.GetUserByVkId(vk_user_id);

            user.Money -= foodToBuy.Price;
            HasPositiveBalance(user);

            var userFoodList = await _userRepository.GetUserFood(user.Id);
            var userFood = user.UserFoods.First(uf => uf.Food.Id == foodToBuy.FoodId);
            if (userFood is not null)
            {
                userFood.Count += 1;
            }
            else
            {
                user.UserFoods.Add(new UserFood
                {
                    UserId = user.Id,
                    FoodId = foodToBuy.FoodId,
                    Count = 1
                });
            }
            await _baseRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task BuyBackground(BuyBackgroundRequest backgroundToBuy, string vk_user_id)
        {
            User user = await _userRepository.GetUserByVkId(vk_user_id);

            if (await _artifactRepository.isBackgroundAbsent(backgroundToBuy.BackgroundId, user.Id))
            {
                throw new InvalidOperationException("User already owns this item.");
            }


            user.Money -= backgroundToBuy.Price;
            HasPositiveBalance(user);

            user.UserBackgrounds.Add(new UserBackground
            {
                UserId = user.Id,
                BackgroundId = backgroundToBuy.BackgroundId,
            });

            await _baseRepository.SaveChangesAsync();
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
