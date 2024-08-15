using FreshCode.ModelsDTO;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PurchaseController : Controller
    {
        private readonly PurchaseUseCase _purchaseUseCase;

        public PurchaseController(PurchaseUseCase purchaseUseCase)
        {
            _purchaseUseCase = purchaseUseCase;
        }
        [HttpPost]
        public async Task BuyArtifact([FromBody] ArtifactDTO artifactToBuy)
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            await _purchaseUseCase.BuyArtifact(artifactToBuy, vk_user_id);
        }

        [HttpPost]
        public async Task BuyFood()
        {
            await _purchaseUseCase.BuyFood();
        }

        //[HttpPost]
        //public async Task BuyBackground()
        //{
        //    await _purchaseUseCase.BuyBackground();
        //}
    }
}
