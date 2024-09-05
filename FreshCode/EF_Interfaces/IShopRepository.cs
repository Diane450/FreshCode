using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.EF_Interfaces
{
    public interface IShopRepository
    {
        Task<List<BackgroundDTO>> GetBackgroundsAsync();
        Task<List<ArtifactDTO>> GetArtifactsAsync();
        Task<List<FoodDTO>> GetFoodAsync();
    }
}
