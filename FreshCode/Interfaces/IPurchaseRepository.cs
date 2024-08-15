using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface IPurchaseRepository
    {
        Task BuyArtifact(ArtifactDTO artifactToBuy, string vk_user_id);
        Task BuyFood(FoodDTO foodToBuy, string? vk_user_id);
    }
}
