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

        [HttpGet("get-value")]
        public async Task<ActionResult<FortuneWheelDropResponse>> GetValue()
        {
            try
            {
                long userId = GetUserId(HttpContext);
                return await _fortuneWheelUseCase.SpinFortuneWheel(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла внутренняя ошибка сервера. Попробуйте позже." });
            }
        }
    }
}
