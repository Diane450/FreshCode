using FreshCode.ModelsDTO;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("battle")]

    public class BattleController (BattleUseCase battleUseCase) : BaseController
    {
        private readonly BattleUseCase _battleUseCase = battleUseCase;

        [HttpGet("join-battle-queue")]
        public async Task<PetDTO> JoinQueue()
        {
            var userId = GetUserId(HttpContext);
            return await _battleUseCase.JoinBattleQueue(userId);
        }
    }
}
