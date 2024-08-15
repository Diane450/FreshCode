using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CreatePetController(CreatePetUseCase createPetUseCase) : ControllerBase
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

        [HttpPost]
        public async Task<PetDTO> CreatePet([FromBody] CreatePetRequest request)
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return await _createPetUseCase.CreatePetAsync(request, vk_user_id);
        }
    }
}
