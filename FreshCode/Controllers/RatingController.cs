using FreshCode.ModelsDTO;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RatingController(UserUseCase userUseCase) : Controller
    {
        private UserUseCase _userUseCase = userUseCase;

        [HttpGet]
        public async Task<List<UserRatingTableDTO>> GetAllUsersRatingTable()
        {
            return await _userUseCase.GetAllUsersRatingTable();
        }

        [HttpGet]
        public async Task<List<ClanRatingTableDTO>> GetClanRatingTable()
        {
            return await _userUseCase.GetClanRatingTable();
        }
    }
}
