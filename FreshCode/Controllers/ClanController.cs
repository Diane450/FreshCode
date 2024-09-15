using FreshCode.Requests;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClanController(ClanUseCase clanUseCase) : BaseController
    {
        private readonly ClanUseCase _clanUseCase = clanUseCase;

        [HttpPost]
        public async Task CreateNewClan([FromBody] string clanName)
        {
            var userId = GetUserId(HttpContext);
            await _clanUseCase.CreateNewClan(clanName, userId);
        }

        [HttpDelete]
        public async Task DeleteClan()
        {
            var userId = GetUserId(HttpContext);
            await _clanUseCase.DeleteClan(userId);
        }


        [HttpPost]
        public async Task AddUserToClan([FromBody] int clanId)
        {
            var userId = GetUserId(HttpContext);
            await _clanUseCase.AddUserToClan(userId); 
        }
    }
}
