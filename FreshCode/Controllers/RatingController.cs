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
        public async Task<PagedList<UserRatingTableDTO>> GetAllUsersRatingTable([FromQuery]QueryParameters queryParameters)
        {
            return await _userUseCase.GetAllUsersRatingTable(queryParameters);
        }

        //[HttpGet("clans")] КЛАНЫ В РАЗРАБОТКЕ
        //public async Task<List<ClanRatingTableDTO>> GetClanRatingTable()
        //{
        //    var userId = GetUserId(HttpContext);
        //    return await _userUseCase.GetClanRatingTable();
        //}

        [HttpGet("friends")]
        public async Task<PagedList<UserRatingTableDTO>> GetFriendsRatingTable()
        {
            var vk_user_id = GetVkId(HttpContext);
            return await _userUseCase.GetFriendsRatingTable(vk_user_id);
        }
    }
}
