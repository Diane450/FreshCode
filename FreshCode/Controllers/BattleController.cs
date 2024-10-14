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

        [HttpGet("find-opponent")]
        public async Task<PetDTO> FindOpponent()
        {
            var userId = GetUserId(HttpContext);
            return await _battleUseCase.FindOpponent(userId);
        }
    }
}
