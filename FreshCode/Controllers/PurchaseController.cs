using FreshCode.Exceptions;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public async Task<ActionResult> BuyArtifact([FromBody] BuyArtifactRequest artifactToBuy)
        {
            try
            {
                string vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
                await _purchaseUseCase.BuyArtifact(artifactToBuy, vk_user_id);
                return Ok();
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (InvalidOperationException exception)
            {
                return Conflict(exception.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> BuyFood([FromBody] BuyFoodRequest foodToBuy)
        {
            try
            {
                var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
                await _purchaseUseCase.BuyFood(foodToBuy, vk_user_id);
                return Ok();
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (InvalidOperationException exception)
            {
                return Conflict(exception.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> BuyBackground([FromBody] BuyBackgroundRequest backgroundToBuy)
        {
            try
            {
                var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
                await _purchaseUseCase.BuyBackground(backgroundToBuy, vk_user_id);
                return Ok();
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (InsufficientFundsException exception)
            {
                return Conflict(exception.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }
    }
}
