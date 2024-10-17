using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("inventory")]
    public class InventoryController(UserUseCase userUseCase, PetsUseCase petsUseCase) : BaseController
    {
        private readonly UserUseCase _userUseCase = userUseCase;
        private readonly PetsUseCase _petsUseCase = petsUseCase;

        [HttpPost("set-background")]
        public async Task<IActionResult> SetBackground([FromBody] long backgroundId)
        {
            try
            {
                long userId = GetUserId(HttpContext);
                var background = await _userUseCase.SetBackground(backgroundId, userId);
                return Ok(background);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }
    }
}
