using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PetsController(PetsUseCase petsUseCase) : ControllerBase
    {
        private readonly PetsUseCase _petsUseCase = petsUseCase;

        [HttpGet]
        public async Task<PetDTO> GetPetAsync()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return await _petsUseCase.GetPetByVkIdAsync(Convert.ToInt32(vk_user_id));
        }

        [HttpPost]
        public async Task<PetDTO> CreatePet([FromBody] CreatePetRequest request)
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return await _petsUseCase.CreatePetAsync(request, vk_user_id);
        }

        [HttpPut]
        public async Task<PetDTO> LevelUp([FromBody] PetDTO pet)
        {
            return await _petsUseCase.LevelUpAsync(pet);
        }
    }
}
