using FreshCode.DbModels;
using FreshCode.Enums;
using FreshCode.Exceptions;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("pet")]
    public class PetsController(PetsUseCase petsUseCase, UserUseCase userUseCase) : BaseController
    {
        private readonly PetsUseCase _petsUseCase = petsUseCase;
        
        private readonly UserUseCase _userUseCase = userUseCase;

        [HttpGet]
        public async Task<PetDTO> GetPetAsync()
        {
            var userId = GetUserId(HttpContext);
            return await _petsUseCase.GetPetByUserIdAsync(userId);
        }

        [HttpPut("levelup")]
        public async Task<PetDTO> LevelUp(long petId)
        {
            return await _petsUseCase.LevelUpAsync(petId);
        }

        [HttpPut("increase-stat")]
        public async Task<ActionResult<PetDTO>> IncreaseStat(IncreaseStatRequest request)
        {
            try
            {
                var userId = GetUserId(HttpContext);
                return await _petsUseCase.IncreaseStat(userId, request);
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

        [HttpPost("create")]
        public async Task<ActionResult<PetDTO>> CreatePet([FromBody] CreatePetRequest request)
        {
            try
            {
                var userId = GetUserId(HttpContext);
                return await _petsUseCase.CreatePetAsync(request, userId);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        //[HttpPut]
        //public async System.Threading.Tasks.Task Feed([FromBody] FeedRequest request)
        //{
        ////TODO: Pet_Bonuses
        //    //await _userUseCase.InventoryDecreaseFoodCountAsync(vk_user_id, request.Food);
        //}
    }
}
