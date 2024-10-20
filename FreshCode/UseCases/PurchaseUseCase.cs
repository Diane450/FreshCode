using FreshCode.DbModels;
using FreshCode.Exceptions;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Responses;

namespace FreshCode.UseCases
{
    public class PurchaseUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IFoodRepository _foodRepository;

        public PurchaseUseCase(IUserRepository userRepository,
            IBaseRepository baseRepository)
            {
                _userRepository = userRepository;
                _baseRepository = baseRepository;
            }
        public async Task<BuyArtifactResponse> BuyArtifact(BuyArtifactRequest artifactToBuy, long userId)
        {
            User user = await _userRepository.GetUserById(userId);

            if (await _userRepository.isArtifactAbsent(artifactToBuy.ArtifactId, user.Id))
            {
                throw new InvalidOperationException("У вас уже есть этот артефакт");
            }

            user.Money -= artifactToBuy.Price;

            HasPositiveBalance(user);

            user.UserArtifacts.Add(new UserArtifact
            {
                UserId = user.Id,
                ArtifactId = artifactToBuy.ArtifactId,
            });

            await _baseRepository.SaveChangesAsync();
            return new BuyArtifactResponse
            {
                Money = user.Money
            };
        }

        public async Task<BuyFoodResponse> BuyFood(BuyFoodRequest foodToBuy, long userId)
        {
            User user = await _userRepository.GetUserById(userId);
            Food food = await _foodRepository.GetFoodById(foodToBuy.FoodId);
            user.Money -= food.Price * foodToBuy.Count;
            HasPositiveBalance(user);

            var userFoodList = _userRepository.GetUserFood(user.Id).ToList();
            var userFood = user.UserFoods.FirstOrDefault(uf => uf.Food.Id == foodToBuy.FoodId);

            int foodCount;
            if (userFood is not null)
            {
                userFood.Count += foodToBuy.Count;
                foodCount = userFood.Count;
            }
            else
            {
                user.UserFoods.Add(new UserFood
                {
                    UserId = user.Id,
                    FoodId = foodToBuy.FoodId,
                    Count = foodToBuy.Count
                });
                foodCount = foodToBuy.Count;
            }
            await _baseRepository.SaveChangesAsync();

            return new BuyFoodResponse
            {
                Money = user.Money,
                FoodCount = foodCount,
                FoodId = foodToBuy.FoodId
            };
        }

        public async System.Threading.Tasks.Task BuyBackground(BuyBackgroundRequest backgroundToBuy, long userId)
        {
            User user = await _userRepository.GetUserById(userId);

            if (await _userRepository.isBackgroundAbsent(backgroundToBuy.BackgroundId, user.Id))
            {
                throw new InvalidOperationException("У вас уже есть данный задний фон");
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
                throw new InvalidOperationException("Недостаточно средств");
            }
        }

        public async Task<BuyWishesResponse> BuyWishes(long userId, int wishCount)
        {
            User user = await _userRepository.GetUserById(userId);

            user.PrimogemsCount -= wishCount * 90;

            HasPositiveBalance(user);

            user.FatesCount += wishCount;
            await _baseRepository.SaveChangesAsync();

            return new BuyWishesResponse
            {
                PrimogemsCount = user.PrimogemsCount,
                WishesCount = user.FatesCount
            };
        }
    }
}
