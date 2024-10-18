using FreshCode.Models;
using FreshCode.ModelsDTO;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("rating")]
    public class RatingController(UserUseCase userUseCase) : BaseController
    {
        private UserUseCase _userUseCase = userUseCase;

        [HttpGet("all-users")]
        public async Task<List<UserRatingTableDTO>> GetAllUsersRatingTable([FromQuery]QueryParameters queryParameters)
        {
            var vk_user_id = GetVkId(HttpContext);

            return await _userUseCase.GetAllUsersRatingTable(queryParameters);
        }

        [HttpGet("clans")]
        public async Task<List<ClanRatingTableDTO>> GetClanRatingTable()
        {
            return await _userUseCase.GetClanRatingTable();
        }

        [HttpGet("friends")]
        public async Task<List<UserRatingTableDTO>> GetFriendsRatingTable()
        {
            //return await _userUseCase.GetFriendsRatingTable(vk_user_id);
            return null;
        }
    }
}
