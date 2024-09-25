using FreshCode.ModelsDTO;
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
    }
}
