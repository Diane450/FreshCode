using FreshCode.ModelsDTO;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("pet-parts")]
    public class PetPartsController(PetPartsUseCase petPartsUseCase) : BaseController
    {
        private readonly PetPartsUseCase _petPartsUseCase = petPartsUseCase;

        [HttpGet("eyes")]
        public async Task<IActionResult> GetEyes()
        {
            try
            {
                return Ok(await _petPartsUseCase.GetEyesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла внутренняя ошибка сервера. Попробуйте позже." });
            }
        }

        [HttpGet("bodies")]
        public async Task<IActionResult> GetBodies()
        {
            try
            {
                return Ok(await _petPartsUseCase.GetBodiesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла внутренняя ошибка сервера. Попробуйте позже." });
            }
        }
    }
}
