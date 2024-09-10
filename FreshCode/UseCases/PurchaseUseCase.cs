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
            IBaseRepository baseRepository,
            IArtifactRepository artifactRepository)
            {
                _userRepository = userRepository;
                _baseRepository = baseRepository;
                _artifactRepository = artifactRepository;
            }
        public async System.Threading.Tasks.Task BuyArtifact(BuyArtifactRequest artifactToBuy, string vk_user_id)
        {
            User user = await _userRepository.GetUserByVkId(vk_user_id);

            user.Money -= artifactToBuy.Price;

            HasPositiveBalance(user);

            user.UserArtifacts.Add(new UserArtifact
            {
                UserId = user.Id,
                ArtifactId = artifactToBuy.ArtifactId,
            });

            await _baseRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task BuyFood(long foodToBuyId, string vk_user_id)
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
            await _baseRepository.SaveChangesAsync();
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
