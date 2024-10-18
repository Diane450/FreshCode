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
        /// <summary>
        /// Получение всей еды в магазине
        /// </summary>
        /// <returns></returns>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>

        [HttpGet("food")]
        public async Task<ActionResult<List<FoodDTO>>> GetFood()
        {
            try
            {
                return await _shopUseCase.GetFoodAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }
        /// <summary>
        /// Получение всех артефактов из магазина
        /// </summary>
        /// <returns></returns>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>
        
        [HttpGet("artifacts")]
        public async Task<ActionResult<List<ArtifactDTO>>> GetArtifacts()
        {
            try
            {
                return await _shopUseCase.GetArtifactsAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }
        /// <summary>
        /// Получение всех задних фонов из магазина
        /// </summary>
        /// <returns></returns>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>

        [HttpGet("backgrounds")]
        public async Task<ActionResult<List<BackgroundDTO>>> GetBackgrounds()
        {
            try
            {
                return await _shopUseCase.GetBackgroundsAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }
    }
}
