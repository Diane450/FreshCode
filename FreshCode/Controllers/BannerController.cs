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

        /// <summary>
        /// Получение информации (Id, когда создан, до какого действует, какие артефакты) о баннере по Id
        /// </summary>
        /// <param name="bannerId">Id баннера</param>
        /// <returns></returns>
        /// <response code="500">Ошибка API</response>

        [HttpGet("{bannerId}")]
        public async Task<ActionResult<BanerDTO>> GetBannerById(long bannerId)
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

        /// <summary>
        /// Дроп артефактов с баннера
        /// </summary>
        /// <param name="wishRequest">Запрос на покрутить баннер</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Недостаточно круток</response>
        /// <response code="500">Ошибка API</response>

        [HttpGet("drop-artifact")]
        public async Task<ActionResult<DropArtifactResponse>> GetArtifacts([FromBody] WishRequest wishRequest)
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
