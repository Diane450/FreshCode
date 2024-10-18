using FreshCode.ModelsDTO;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("user")]

    public class UserController(UserUseCase userUseCase):BaseController
    {
        private readonly UserUseCase _userUseCase = userUseCase;

        /// <summary>
        /// Получение игровой информации о пользователе
        /// </summary>
        /// <returns></returns>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("game-info")]
        public async Task <ActionResult<UserDTO>> GetUserGameInfo()
        {
            try
            {
                var userId = GetUserId(HttpContext);
                return await _userUseCase.GetUserGameInfo(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }

        //[HttpGet("tasks")]
        //public async Task<List<TaskDTO>> GetUserTasks()
        //{
        //    var userId = GetUserId(HttpContext);
        //    return await _userUseCase.GetUserTasks(userId);
        //}

        /// <summary>
        /// Получение истории круток из баннера
        /// </summary>
        /// <param name="bannerId">Id баннера</param>
        /// <returns></returns>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>

        [HttpGet("artifact-history/banner/{bannerId}")]
        public async Task<ActionResult<List<ArtifactHistoryDTO>>> GetArtifactHistory(long bannerId)
        {
            try
            {
                var userId = GetUserId(HttpContext);
                return await _userUseCase.GetArtifactHistory(userId, bannerId);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }
        /// <summary>
        /// Получение списка еды пользователя 
        /// </summary>
        /// <returns></returns>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>

        [HttpGet("food")]
        public async Task<ActionResult<List<UserFoodDTO>>> GetUserFood()
        {
            try
            {
                var userId = GetUserId(HttpContext);
                return await _userUseCase.GetUserFood(userId);
            }
            catch (Exception)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }
        /// <summary>
        /// Получение списка артефактов пользователя 
        /// </summary>
        /// <returns></returns>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>

        [HttpGet("artifacts")]
        public async Task<ActionResult<List<ArtifactDTO>>> GetUserArtifact()
        {
            try
            {
                var userId = GetUserId(HttpContext);
                return await _userUseCase.GetUserArtifact(userId);
            }
            catch (Exception)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }
        /// <summary>
        /// Получение списка задних фонов пользователя 
        /// </summary>
        /// <returns></returns>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>

        [HttpGet("backgrounds")]
        public async Task<ActionResult<List<BackgroundDTO>>> GetUserBackgrounds()
        {
            try
            {
                var userId = GetUserId(HttpContext);
                return await _userUseCase.GetUserBackgrounds(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }
    }
}