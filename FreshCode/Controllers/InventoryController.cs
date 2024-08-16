using FreshCode.ModelsDTO;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InventoryController(InventoryUseCase inventoryUseCase) : Controller
    {
        public InventoryUseCase _inventoryUseCase { get; set; } = inventoryUseCase;

        [HttpPost]
        public async Task<IActionResult> SetBackground([FromBody] BackgroundDTO backgroundDTO)
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            await _inventoryUseCase.SetBackground(backgroundDTO, vk_user_id);
            return Ok();
        }
    }
}
