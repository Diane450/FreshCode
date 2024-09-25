using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InventoryController(UserUseCase userUseCase, PetsUseCase petsUseCase) : BaseController
    {
        private readonly UserUseCase _userUseCase = userUseCase;
        private readonly PetsUseCase _petsUseCase = petsUseCase;

        [HttpPost]
        public async Task<IActionResult> SetBackground([FromBody] long backgroundId)
        {
            try
            {
                long userId = GetUserId(HttpContext);
                await _userUseCase.SetBackground(backgroundId, userId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
