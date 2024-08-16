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
    public class PetsController(PetsUseCase petsUseCase, UserUseCase userUseCase) : ControllerBase
    {
        private readonly PetsUseCase _petsUseCase = petsUseCase;
        
        private readonly UserUseCase _userUseCase = userUseCase;


        [HttpGet]
        public async Task<PetDTO> GetPetAsync()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return await _petsUseCase.GetPetByVkIdAsync(Convert.ToInt32(vk_user_id));
        }

        [HttpPut]
        public async Task<PetDTO> LevelUp([FromBody] PetDTO pet)
        {
            return await _petsUseCase.LevelUpAsync(pet);
        }

        [HttpPut]
        public async System.Threading.Tasks.Task ChangeArtifact([FromBody] PetDTO pet)
        {
            await _petsUseCase.ChangePetsArtifact(pet);
        }

        //[HttpPut]
        //public async System.Threading.Tasks.Task Feed([FromBody] FeedRequest request)
        //{
        // TODO: Pet_Bonuses
        //    var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
        //    await _petsUseCase.FeedAsync(request);
        //    await _userUseCase.InventoryDecreaseFoodCountAsync(vk_user_id, request.Food);
        //}
    }
}
