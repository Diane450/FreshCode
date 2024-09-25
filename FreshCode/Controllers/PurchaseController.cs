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
    [Route("buy")]
    public class PurchaseController : BaseController
    {
        private readonly PurchaseUseCase _purchaseUseCase;

        public PurchaseController(PurchaseUseCase purchaseUseCase)
        {
            _purchaseUseCase = purchaseUseCase;
        }

        [HttpPost("artifact")]
        public async Task<ActionResult> BuyArtifact([FromBody] BuyArtifactRequest artifactToBuy)
        {
            try
            {
                var userId = GetUserId(HttpContext);
                await _purchaseUseCase.BuyArtifact(artifactToBuy, userId);
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

        [HttpPost("food")]
        public async Task<ActionResult> BuyFood([FromBody] BuyFoodRequest foodToBuy)
        {
            try
            {
                var userId = GetUserId(HttpContext);
                await _purchaseUseCase.BuyFood(foodToBuy, userId);
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

        [HttpPost("background")]
        public async Task<ActionResult> BuyBackground([FromBody] BuyBackgroundRequest backgroundToBuy)
        {
            try
            {
                var userId = GetUserId(HttpContext);
                await _purchaseUseCase.BuyBackground(backgroundToBuy, userId);
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
