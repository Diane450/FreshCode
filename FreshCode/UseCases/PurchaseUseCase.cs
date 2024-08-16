using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;

namespace FreshCode.UseCases
{
    public class PurchaseUseCase
    {
        private readonly IPurchaseRepository _purchaseRepository;

        private readonly IUserRepository _userRepository;

        public PurchaseUseCase(IPurchaseRepository purchaseRepository, IUserRepository userRepository)
        {
            _purchaseRepository = purchaseRepository;
            _userRepository = userRepository;
        }
        public async System.Threading.Tasks.Task BuyArtifact(ArtifactDTO artifactToBuy, string vk_user_id)
        {
            User user = await _userRepository.GetUserByVkId(vk_user_id);
            await _purchaseRepository.BuyArtifact(artifactToBuy, user);
        }

        public async System.Threading.Tasks.Task BuyFood(FoodDTO foodToBuy, string vk_user_id)
        {
            User user = await _userRepository.GetUserByVkId(vk_user_id);

            await _purchaseRepository.BuyFood(foodToBuy, user);
        }

        public async System.Threading.Tasks.Task BuyBackground(BackgroundDTO backgroundToBuy, string vk_user_id)
        {
            User user = await _userRepository.GetUserByVkId(vk_user_id);

            await _purchaseRepository.BuyBackground(backgroundToBuy, user);
        }
    }
}
