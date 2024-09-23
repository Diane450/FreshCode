using FreshCode.Exceptions;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CreatePetController(CreatePetUseCase createPetUseCase) : BaseController
    {
        private readonly CreatePetUseCase _createPetUseCase = createPetUseCase;


        [HttpGet]
        public async Task<List<EyeDTO>> GetEyes()
        {
            return await _createPetUseCase.GetEyesAsync();
        }

        [HttpGet]
        public async Task<List<BodyDTO>> GetBodies()
        {
            return await _createPetUseCase.GetBodiesAsync();
        }
    }
}
