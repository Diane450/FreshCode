using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("shop")]
    public class ShopController(ShopUseCase shopUseCase) : Controller
    {
        private readonly ShopUseCase _shopUseCase = shopUseCase;

        [HttpGet("food")]
        public async Task<List<FoodDTO>> GetFood()
        {
            return await _shopUseCase.GetFoodAsync();
        }

        [HttpGet("artifacts")]
        public async Task<List<ArtifactDTO>> GetArtifacts()
        {
            return await _shopUseCase.GetArtifactsAsync();
        }

        [HttpGet("backgrounds")]
        public async Task<List<BackgroundDTO>> GetBackgrounds()
        {
            return await _shopUseCase.GetBackgroundsAsync();
        }
    }
}
