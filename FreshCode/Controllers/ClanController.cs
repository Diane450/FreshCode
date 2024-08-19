using FreshCode.Requests;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClanController(ClanUseCase clanUseCase) : Controller
    {
        private readonly ClanUseCase _clanUseCase = clanUseCase;

        [HttpPost]
        public async Task CreateNewClan([FromBody] string clanName)
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            await _clanUseCase.CreateNewClan(clanName, vk_user_id);
        }


        [HttpPost]
        public async Task DeleteClan()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            await _clanUseCase.DeleteClan(vk_user_id);
        }
    }
}
