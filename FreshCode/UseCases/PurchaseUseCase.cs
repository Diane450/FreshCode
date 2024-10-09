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

        public PurchaseUseCase(IUserRepository userRepository,
            IBaseRepository baseRepository)
            {
                _userRepository = userRepository;
                _baseRepository = baseRepository;
            }
        public async System.Threading.Tasks.Task BuyArtifact(BuyArtifactRequest artifactToBuy, long userId)
        {
            User user = await _userRepository.GetUserById(userId);

            if (await _userRepository.isArtifactAbsent(artifactToBuy.ArtifactId, user.Id))
            {
                throw new InvalidOperationException("Пользователь уже имеет данный артефакт");
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

        public async System.Threading.Tasks.Task BuyFood(BuyFoodRequest foodToBuy, long userId)
        {
            User user = await _userRepository.GetUserById(userId);

            user.Money -= foodToBuy.Price * foodToBuy.Count;
            HasPositiveBalance(user);

            var userFoodList = _userRepository.GetUserFood(user.Id).ToList();
            var userFood = user.UserFoods.First(uf => uf.Food.Id == foodToBuy.FoodId);
            if (userFood is not null)
            {
                userFood.Count += foodToBuy.Count;
            }
            else
            {
                user.UserFoods.Add(new UserFood
                {
                    UserId = user.Id,
                    FoodId = foodToBuy.FoodId,
                    Count = foodToBuy.Count
                });
            }
            await _baseRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task BuyBackground(BuyBackgroundRequest backgroundToBuy, long userId)
        {
            User user = await _userRepository.GetUserById(userId);

            if (await _userRepository.isBackgroundAbsent(backgroundToBuy.BackgroundId, user.Id))
            {
                throw new InvalidOperationException("Пользователь уже имеет данный фон");
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
                throw new InvalidOperationException("User does not have enough funds");
            }
        }
    }
}
