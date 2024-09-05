using FreshCode.DbModels;
using FreshCode.EF_Interfaces;
using FreshCode.ModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.UseCases
{
    public class ShopUseCase(IShopRepository shopRepository)
    {
        private readonly IShopRepository _shopRepository = shopRepository;

        public async Task<List<FoodDTO>> GetFoodAsync()
        {
            return await _shopRepository.GetFoodAsync();
        }

        public async Task<List<ArtifactDTO>> GetArtifactsAsync()
        {
            return await _shopRepository.GetArtifactsAsync();
        }

        public async Task<List<BackgroundDTO>> GetBackgroundsAsync()
        {
            return await _shopRepository.GetBackgroundsAsync();
        }
    }
}
