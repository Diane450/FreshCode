using FreshCode.ModelsDTO;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class UserController(UserUseCase userUseCase):BaseController
    {
        private readonly UserUseCase _userUseCase = userUseCase;
        
        [HttpGet]
        public async Task <UserDTO> GetUserGameInfo()
        {
            var userId = GetUserId(HttpContext);
            return await _userUseCase.GetUserGameInfo(userId);
        }

        [HttpGet]
        public async Task<List<TaskDTO>> GetUserTasks()
        {
            var userId = GetUserId(HttpContext);
            return await _userUseCase.GetUserTasks(userId);
        }

        [HttpGet]
        public async Task<List<ArtifactHistoryDTO>> GetArtifactHistory()
        {
            var userId = GetUserId(HttpContext);
            return await _userUseCase.GetArtifactHistory(userId);
        }

        [HttpGet]
        public async Task<List<UserFoodDTO>> GetUserFood()
        {
            var userId = GetUserId(HttpContext);
            return await _userUseCase.GetUserFood(userId);
        }

        [HttpGet]
        public async Task<List<ArtifactDTO>> GetUserArtifact()
        {
            var userId = GetUserId(HttpContext);
            return await _userUseCase.GetUserArtifact(userId);
        }

        [HttpGet]
        public async Task<List<BackgroundDTO>> GetUserBackgrounds()
        {
            var userId = GetUserId(HttpContext);
            return await _userUseCase.GetUserBackgrounds(userId);
        }
    }
}
