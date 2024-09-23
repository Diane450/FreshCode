using FreshCode.Requests;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("clan")]
    public class ClanController(ClanUseCase clanUseCase) : BaseController
    {
        private readonly ClanUseCase _clanUseCase = clanUseCase;

        [HttpPost("create")]
        public async Task CreateNewClan([FromBody] string clanName)
        {
            var userId = GetUserId(HttpContext);
            await _clanUseCase.CreateNewClan(clanName, userId);
        }

        [HttpDelete("delete")]
        
        public async Task DeleteClan([FromBody] int clanId)
        {
            var userId = GetUserId(HttpContext);
            await _clanUseCase.DeleteClan(userId);
        }


        [HttpPost("{clanId}/add-user")]
        public async Task AddUserToClan(long clanId, [FromBody] AddUserToClanRequest request)
        {
            var userId = GetUserId(HttpContext);
            await _clanUseCase.AddUserToClan(userId, clanId, request); 
        }
    }
}
