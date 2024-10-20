using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

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
        /// <response code="404">У пользователя нет еды</response>

        [HttpGet("food")]
        public async Task<ActionResult<List<UserFoodDTO>>> GetUserFood()
        {
            try
            {
                var userId = GetUserId(HttpContext);
                var food =  await _userUseCase.GetUserFood(userId);

                if (food == null || !food.Any()) // Если еды нет
                {
                    return NotFound("У вас нет еды");
                }
                return Ok(food);
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
        /// <response code="404">У пользователя нет артефактов</response>

        [HttpGet("artifacts")]
        public async Task<ActionResult<List<ArtifactDTO>>> GetUserArtifact()
        {
            try
            {
                var userId = GetUserId(HttpContext);
                var artifacts = await _userUseCase.GetUserArtifact(userId);

                if (artifacts == null || !artifacts.Any()) // Если еды нет
                {
                    return NotFound("У вас нет артефактов");
                }
                return Ok(artifacts);

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

        /// <summary>
        /// Установка заднего фона для пользователя
        /// </summary>
        /// <param name="backgroundId">Id заднего фона</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">У пользователя нет выбрнного фона</response>
        /// <response code="500">Ошибка API</response>

        [HttpPost("set-background")]
        public async Task<ActionResult<BackgroundDTO>> SetBackground([FromBody] long backgroundId)
        {
            try
            {
                long userId = GetUserId(HttpContext);
                var background = await _userUseCase.SetBackground(backgroundId, userId);
                return Ok(background);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }

    }
}