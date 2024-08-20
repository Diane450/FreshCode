using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InventoryController(InventoryUseCase inventoryUseCase, PetsUseCase petsUseCase) : Controller
    {
        public InventoryUseCase _inventoryUseCase { get; set; } = inventoryUseCase;
        private readonly PetsUseCase _petsUseCase = petsUseCase;

        [HttpPost]
        public async Task<IActionResult> SetBackground([FromBody] long backgroundId)
        {
            try
            {
                var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
                await _inventoryUseCase.SetBackground(backgroundId, vk_user_id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetArtifact([FromBody] SetArtifactRequest setArtifactRequest)
        {
            await _petsUseCase.SetArtifact(setArtifactRequest);
            return Ok();
        }
    }
}
