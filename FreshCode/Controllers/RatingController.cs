using FreshCode.ModelsDTO;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("rating")]
    public class RatingController(UserUseCase userUseCase) : Controller
    {
        private UserUseCase _userUseCase = userUseCase;

        [HttpGet("all-users")]
        public async Task<List<UserRatingTableDTO>> GetAllUsersRatingTable()
        {
            return await _userUseCase.GetAllUsersRatingTable();
        }

        [HttpGet("clans")]
        public async Task<List<ClanRatingTableDTO>> GetClanRatingTable()
        {
            return await _userUseCase.GetClanRatingTable();
        }

        [HttpGet("friends")]
        public async Task<List<UserRatingTableDTO>> GetFriendsRatingTable()
        {
            string vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            return await _userUseCase.GetFriendsRatingTable(vk_user_id);
        }
    }
}
