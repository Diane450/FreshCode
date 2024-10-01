using FreshCode.ModelsDTO;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("user")]

    public class UserController(UserUseCase userUseCase):BaseController
    {
        private readonly UserUseCase _userUseCase = userUseCase;
        
        [HttpGet("game-info")]
        public async Task <UserDTO> GetUserGameInfo()
        {
            var userId = GetUserId(HttpContext);
            return await _userUseCase.GetUserGameInfo(userId);
        }

        [HttpGet("tasks")]
        public async Task<List<TaskDTO>> GetUserTasks()
        {
            var userId = GetUserId(HttpContext);
            return await _userUseCase.GetUserTasks(userId);
        }

        [HttpGet("artifact-history/banner/{bannerId}")]
        public async Task<List<ArtifactHistoryDTO>> GetArtifactHistory(long bannerId)
        {
            var userId = GetUserId(HttpContext);
            return await _userUseCase.GetArtifactHistory(userId, bannerId);
        }

        [HttpGet("food")]
        public async Task<List<UserFoodDTO>> GetUserFood()
        {
            var userId = GetUserId(HttpContext);
            return await _userUseCase.GetUserFood(userId);
        }

        [HttpGet("artifacts")]
        public async Task<List<ArtifactDTO>> GetUserArtifact()
        {
            var userId = GetUserId(HttpContext);
            return await _userUseCase.GetUserArtifact(userId);
        }

        [HttpGet("backgrounds")]
        public async Task<List<BackgroundDTO>> GetUserBackgrounds()
        {
            var userId = GetUserId(HttpContext);
            return await _userUseCase.GetUserBackgrounds(userId);
        }
    }
}
