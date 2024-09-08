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
            return await _userRepositoryDapper.GetArtifactHistory(Convert.ToInt64(vk_user_id));
        }

        [HttpGet]
        public async Task<List<UserFoodDTO>> GetUserFood()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return await _userRepositoryDapper.GetUserFood(Convert.ToInt64(vk_user_id));
        }

        [HttpGet]
        public async Task<List<ArtifactDTO>> GetUserArtifacts()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return await _userRepositoryDapper.GetUserArtifacts(Convert.ToInt64(vk_user_id));
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
