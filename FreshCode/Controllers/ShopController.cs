using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ShopController(ShopUseCase shopUseCase) : Controller
    {
        private readonly ShopUseCase _shopUseCase = shopUseCase;

        [HttpGet]
        public async Task<List<FoodDTO>> GetFood()
        {
            return await _shopUseCase.GetFoodAsync();
        }

        [HttpGet]
        public async Task<List<ArtifactDTO>> GetArtifacts()
        {
            return await _shopUseCase.GetArtifactsAsync();
        }

        [HttpGet]
        public async Task<List<BackgroundDTO>> GetBackgrounds()
        {
            return await _shopUseCase.GetBackgroundsAsync();
        }
    }
}
