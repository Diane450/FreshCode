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
            var user_id = GetUserId(HttpContext);
            return await _userUseCase.GetUserGameInfo(user_id);
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
            var user_id = Request.Cookies["userId"]!;
            return await _userUseCase.GetArtifactHistory(user_id);
        }

        [HttpGet]
        public async Task<List<UserFoodDTO>> GetUserFood()
        {
            var user_id = "";
            return await _userUseCase.GetUserFood(user_id);
        }

        [HttpGet]
        public async Task<List<ArtifactDTO>> GetUserArtifact()
        {
            var user_id = Request.Cookies["userId"]!;
            return await _userUseCase.GetUserArtifact(user_id);
        }

        [HttpGet]
        public async Task<List<BackgroundDTO>> GetUserBackgrounds()
        {
            var user_id = Request.Cookies["userId"]!;
            return await _userUseCase.GetUserBackgrounds(user_id);
        }
    }
}
