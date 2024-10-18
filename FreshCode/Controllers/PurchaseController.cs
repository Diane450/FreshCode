using FreshCode.Exceptions;
using FreshCode.Requests;
using FreshCode.Responses;
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

        /// <summary>
        /// Купить артефакт
        /// </summary>
        /// <returns></returns>
        /// <param name="artifactToBuy">покупаемый артефакт</param>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="409">Недостаточно средств(денег)</response>

        [HttpPost("artifact")]
        public async Task<ActionResult<BuyArtifactResponse>> BuyArtifact([FromBody] BuyArtifactRequest artifactToBuy)
        {
            try
            {
                var userId = GetUserId(HttpContext);
                return await _purchaseUseCase.BuyArtifact(artifactToBuy, userId);
            }
            catch (ArgumentException exception)
            {
                return NotFound(exception.Message);
            }
            catch (InvalidOperationException exception)
            {
                return Conflict(exception.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }
        /// <summary>
        /// Купить еду
        /// </summary>
        /// <returns></returns>
        /// <param name="foodToBuy">покупаемая еда</param>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="409">Недостаточно средств(денег)</response>

        [HttpPost("food")]
        public async Task<ActionResult<BuyFoodResponse>> BuyFood([FromBody] BuyFoodRequest foodToBuy)
        {
            try
            {
                var userId = GetUserId(HttpContext);
                return await _purchaseUseCase.BuyFood(foodToBuy, userId);
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

        /// <summary>
        /// Купить задний фон
        /// </summary>
        /// <returns></returns>
        /// <param name="backgroundToBuy">покупаемый задний фон</param>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="409">Недостаточно средств(денег)</response>

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
        /// <summary>
        /// Купить крутки
        /// </summary>
        /// <returns></returns>
        /// <param name="wishCount">кол-во круток</param>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="409">Недостаточно средств(примогемов)</response>

        [HttpPost("wishes")]
        public async Task<ActionResult<BuyWishesResponse>> BuyWishes([FromBody]int wishCount)
        {
            try
            {
                var userId = GetUserId(HttpContext);
                return await _purchaseUseCase.BuyWishes(userId, wishCount);
            }
            catch (InsufficientFundsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }
    }
}
