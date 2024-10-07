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
                return BadRequest(ex.Message);
            }
        }
    }
}
