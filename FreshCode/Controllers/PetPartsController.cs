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
        public async Task<List<EyeDTO>> GetEyes()
        {
            return await _petPartsUseCase.GetEyesAsync();
        }

        [HttpGet("bodies")]
        public async Task<List<BodyDTO>> GetBodies()
        {
            return await _petPartsUseCase.GetBodiesAsync();
        }
    }
}
