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
        
        /// <summary>
        /// Чтение данных обо всех глазах для создания питомца
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="500">Ошибка API</response>

        [HttpGet("eyes")]
        public async Task<ActionResult<EyeDTO>> GetEyes()
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

        /// <summary>
        /// Чтение данных обо всех телах для создания питомца
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="500">Ошибка API</response>

        [HttpGet("bodies")]
        public async Task<ActionResult<BodyDTO>> GetBodies()
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
