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
        /// <summary>
        /// Рейтинг среди всех пользователей
        /// </summary>
        /// <returns></returns>
        /// <param name="queryParameters">параметры пагинации</param>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>

        [HttpGet("all-users")]
        public async Task<ActionResult<PagedList<UserRatingTableDTO>>> GetAllUsersRatingTable([FromQuery]QueryParameters queryParameters)
        {
            try
            {
                return await _userUseCase.GetAllUsersRatingTable(queryParameters);
            }
            catch (Exception ex )
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }

        //[HttpGet("clans")] КЛАНЫ В РАЗРАБОТКЕ
        //public async Task<List<ClanRatingTableDTO>> GetClanRatingTable()
        //{
        //    var userId = GetUserId(HttpContext);
        //    return await _userUseCase.GetClanRatingTable();
        //}
        /// <summary>
        /// Рейтинг среди друзей пользователя
        /// </summary>
        /// <returns></returns>
        /// <param name="queryParameters">параметры пагинации</param>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>

        [HttpGet("friends")]
        public async Task<ActionResult<PagedList<UserRatingTableDTO>>> GetFriendsRatingTable([FromQuery] QueryParameters queryParameters)
        {
            try
            {
                var vk_user_id = GetVkId(HttpContext);
                return await _userUseCase.GetFriendsRatingTable(queryParameters, vk_user_id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }
    }
}
