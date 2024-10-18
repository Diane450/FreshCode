using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Responses;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("fortune-wheel")]
    public class FortuneWheelController(FortuneWheelUseCase fortuneWheelUseCase) : BaseController
    {
        private readonly FortuneWheelUseCase _fortuneWheelUseCase = fortuneWheelUseCase;

        /// <summary>
        /// Получение информации о том, может ли пользователь покрутить колесо фортуны
        /// </summary>
        /// <returns>Возвращает:canSpin — можно ли крутить, timeUntilNextSpin — через сколько часов можно сделать след крутку (если canSpin == false)</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="500">Ошибка API</response>


        [HttpGet("can-spin")]
        public async Task<ActionResult> IsSpinAvailable()
        {
            try
            {
                long userId = GetUserId(HttpContext);
                var result = await _fortuneWheelUseCase.IsSpinAvailable(userId);
                
                return Ok(new
                {
                    canSpin = result.Item1,
                    timeUntilNextSpin = result.Item2
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Произошла внутренняя ошибка сервера. Попробуйте позже." });
            }
        }

        /// <summary>
        /// Получение бонуса с колеса фортуны
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Пользователю нельзя совершать крутку, слишком рано, не прошло 24 часа с последнего момента крутки</response>
        /// <response code="500">Ошибка API</response>

        [HttpGet("get-value")]
        public async Task<ActionResult<FortuneWheelDropResponse>> GetValue()
        {
            try
            {
                long userId = GetUserId(HttpContext);
                return await _fortuneWheelUseCase.SpinFortuneWheel(userId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла внутренняя ошибка сервера. Попробуйте позже." });
            }
        }
    }
}
