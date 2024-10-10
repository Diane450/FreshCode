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
        public async Task<BanerDTO> GetBannerById(long bannerId)
        {
            return await _bannerUseCase.GetBannerById(bannerId);
        }

        [HttpGet("drop-artifact")]
        public async Task<DropArtifactResponse> GetArtifacts([FromBody] WishRequest wishRequest)
        {
            long userId = GetUserId(HttpContext);
            return await _bannerUseCase.GetArtifact(userId, wishRequest);
        }
    }
}
