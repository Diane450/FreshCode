using FreshCode.Hubs;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("battle")]

    public class BattleController (BattleUseCase battleUseCase, IHubContext<BattleHub> hubContext) : BaseController
    {
        private readonly IHubContext<BattleHub> _hubContext = hubContext;

        private readonly BattleUseCase _battleUseCase = battleUseCase;

        [HttpGet("join-battle-queue")]
        public async Task JoinQueue()
        {
            var userId = GetUserId(HttpContext);
            await _battleUseCase.JoinBattleQueue(userId);
        }

        [HttpPost("attack")]
        public async Task<IActionResult> Attack([FromBody] AttackRequest request)
        {
            await _hubContext.Clients.Group(request.BattleId.ToString()).SendAsync("Attack", request.BattleId, request.AttackerId, request.DefenderId);
            return Ok();
        }
    }
}
