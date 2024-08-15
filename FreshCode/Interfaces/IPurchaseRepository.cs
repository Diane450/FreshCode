using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface IPurchaseRepository
    {
        System.Threading.Tasks.Task BuyArtifact(ArtifactDTO artifactToBuy, User user);
        System.Threading.Tasks.Task BuyFood(FoodDTO foodToBuy, User user);
        System.Threading.Tasks.Task BuyBackground(BackgroundDTO backgroundToBuy, User user);
    }
}
