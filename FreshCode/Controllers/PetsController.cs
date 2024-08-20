using FreshCode.DbModels;
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
    public class PetsController(PetsUseCase petsUseCase, UserUseCase userUseCase) : ControllerBase
    {
        private readonly PetsUseCase _petsUseCase = petsUseCase;
        
        private readonly UserUseCase _userUseCase = userUseCase;


        [HttpGet]
        public async Task<PetDTO> GetPetAsync()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return await _petsUseCase.GetPetByIdAsync(vk_user_id);
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

        [HttpPut]
        public async Task<ActionResult<PetDTO>> IncreaseHealth([FromBody] PetDTO pet)
        {
            try
            {
                var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
                return await _petsUseCase.IncreaseHealth(vk_user_id, pet);
            }
            catch (InsufficientFundsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<PetDTO>> IncreaseStat([FromBody] IncreaseStatRequest statRequest)
        {
            try
            {
                var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
                return await _petsUseCase.IncreaseStat(vk_user_id, statRequest);
            }
            catch (InsufficientFundsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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
