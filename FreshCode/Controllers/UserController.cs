using FreshCode.Dapper_Interfaces;
using FreshCode.ModelsDTO;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class UserController(IUserRepositoryDapper userRepository) : Controller
    {
        private readonly IUserRepositoryDapper _userRepositoryDapper = userRepository;

        [HttpGet]
        public async Task<List<TaskDTO>> GetUserTasks()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return await _userRepositoryDapper.GetUserTasks(Convert.ToInt64(vk_user_id));
        }

        [HttpGet]
        public async Task <UserDTO> GetUserGameInfo()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return await _userRepositoryDapper.GetUserGameInfo(Convert.ToInt64(vk_user_id));
        }

        [HttpGet]
        public async Task<List<ArtifactHistoryDTO>> GetArtifactHistory()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return null;
            //return await _userUseCase.GetArtifactHistory(vk_user_id);
        }

        [HttpGet]
        public async Task<List<UserFoodDTO>> GetUserFood()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            //return await _userUseCase.GetUserFood(vk_user_id);
            return null;
        }

        [HttpGet]
        public async Task<List<ArtifactDTO>> GetUserArtifact()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            //return await _userUseCase.GetUserArtifact(vk_user_id);
            return null;
        }

        [HttpGet]
        public async Task<List<BackgroundDTO>> GetUserBackgrounds()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            //return await _userUseCase.GetUserBackgrounds(vk_user_id);
            return null;
        }
    }
}
