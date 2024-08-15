using FreshCode.Interfaces;
using FreshCode.ModelsDTO;

namespace FreshCode.UseCases
{
    public class PurchaseUseCase
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseUseCase(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task BuyArtifact(ArtifactDTO artifactToBuy, string vk_user_id)
        {
            await _purchaseRepository.BuyArtifact(artifactToBuy, vk_user_id);
        }
    }
}
