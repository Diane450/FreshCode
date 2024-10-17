using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Responses;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("banner")]
    public class BannerController(BannerUseCase bannerUseCase) : BaseController
    {
        private readonly BannerUseCase _bannerUseCase = bannerUseCase;

        [HttpGet("{bannerId}")]
        public async Task<IActionResult> GetBannerById(long bannerId)
        {
            try
            {
                return Ok(await _bannerUseCase.GetBannerById(bannerId));
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Произошла внутренняя ошибка сервера. Попробуйте позже." });
            }
        }

        [HttpGet("drop-artifact")]
        public async Task<IActionResult> GetArtifacts([FromBody] WishRequest wishRequest)
        {
            try
            {
                long userId = GetUserId(HttpContext);

                return Ok(await _bannerUseCase.GetArtifact(userId, wishRequest));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Произошла внутренняя ошибка сервера. Попробуйте позже." });
            }
        }
    }
}
