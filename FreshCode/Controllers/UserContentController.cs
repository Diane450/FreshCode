using FreshCode.ModelsDTO;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class UserContentController(UserUseCase userUseCase) : Controller
    {

        private readonly UserUseCase _userUseCase = userUseCase;

        [HttpGet]
        public async Task <UserDTO> GetUserGameInfo()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return await _userUseCase.GetUserGameInfo(vk_user_id);
        }

        [HttpGet]
        public async Task<List<TaskDTO>> GetUserTasks()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return await _userUseCase.GetUserTasks(vk_user_id);
        }

        [HttpGet]
        public async Task<List<ArtifactHistoryDTO>> GetArtifactHistory()
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return await _userUseCase.GetArtifactHistory(vk_user_id);
        }
    }
}
