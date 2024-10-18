using FreshCode.Exceptions;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Responses;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("pet")]
    public class PetsController(PetsUseCase petsUseCase, UserUseCase userUseCase) : BaseController
    {
        private readonly PetsUseCase _petsUseCase = petsUseCase;
        
        private readonly UserUseCase _userUseCase = userUseCase;

        /// <summary>
        /// Чтение данных о питомце пользователя
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">У пользователя нет питомца</response>
        /// <response code="500">Ошибка API</response>

        [HttpGet]
        public async Task<ActionResult<PetDTO>> GetPetAsync()
        {
            try
            {
                var userId = GetUserId(HttpContext);
                return Ok(await _petsUseCase.GetPetByUserIdAsync(userId));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500,"Ошибка на сервере, попробуйте позже");
            }
        }

        /// <summary>
        /// Получение информации об артефактах питомца
        /// </summary>
        /// <returns></returns>
        /// <param name="petId">Id питомца</param>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">У пользователя нет питомца</response>
        /// <response code="500">Ошибка API</response>

        [HttpGet("artifacts")]
        public async Task<List<ArtifactDTO>> GetPetArtifacts([FromBody] long petId)
        {
            return await _petsUseCase.GetPetArtifacts(petId);
        }

        /// <summary>
        /// Получение информации о статах питомца вместе с бонусами
        /// </summary>
        /// <returns></returns>
        /// <param name="petId">Id питомца</param>

        [HttpGet("total-stats")]
        public async Task<PetStatResponse> GetPetStats([FromBody] long petId)
        {
            return await _petsUseCase.GetPetStats(petId);
        }

        /// <summary>
        /// Обновление(повышение) уровня питомца
        /// </summary>
        /// <returns></returns>
        /// <param name="petId">Id питомца</param>

        [HttpPut("levelup")]
        public async Task<PetDTO> LevelUp(long petId)
        {
            return await _petsUseCase.LevelUpAsync(petId);
        }

        /// <summary>
        /// Обновление(повышение) уровня питомца
        /// </summary>
        /// <returns></returns>
        /// <param name="petId">Id питомца</param>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="409">Нехватка средств(очков для повышения статов)</response>
        /// <response code="400">Пользователь или питомцы не найдены</response>
        /// <response code="500">Ошибка API</response>
        [HttpPut("increase-stat")]
        public async Task<ActionResult<PetDTO>> IncreaseStat(IncreaseStatRequest request)
        {
            try
            {
                var userId = GetUserId(HttpContext);
                return await _petsUseCase.IncreaseStat(userId, request);
            }
            catch (InsufficientFundsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }

        }

        /// <summary>
        /// Создание нового питомца
        /// </summary>
        /// <returns></returns>
        /// <param name="request">Запрос на создание питомца</param>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>

        [HttpPost("create")]
        public async Task<ActionResult<PetDTO>> CreatePet([FromBody] CreatePetRequest request)
        {
            try
            {
                var userId = GetUserId(HttpContext);
                return await _petsUseCase.CreatePetAsync(request, userId);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }

        /// <summary>
        /// Кормить нового питомца
        /// </summary>
        /// <returns></returns>
        /// <param name="request">Запрос на покормить</param>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Недостаточно еды или питомец наелся</response>

        [HttpPut("feed")]
        public async Task<ActionResult> Feed([FromBody] FeedRequest request)
        {
            try
            {
                var userId = GetUserId(HttpContext);
                await _petsUseCase.Feed(userId, request);
                return Ok();

            }
            catch (ArgumentException ex)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }

        /// <summary>
        /// Устновить новый артефакт
        /// </summary>
        /// <returns></returns>
        /// <param name="setArtifactRequest">Запрос на установить новый артефакт</param>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>

        [HttpPut("set-artifact")]
        public async Task<ActionResult<PetDTO>> SetArtifact([FromBody] SetArtifactRequest setArtifactRequest)
        {
            try
            {
                return Ok(await _petsUseCase.SetArtifact(setArtifactRequest));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }
        /// <summary>
        /// Устновить новый артефакт
        /// </summary>
        /// <returns></returns>
        /// <param name="removeArtifactRequest">Запрос на снятие артефакта</param>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>

        [HttpPut("remove-artifact")]
        public async Task<ActionResult<PetDTO>> RemoveArtifact([FromBody] RemoveArtifactRequest removeArtifactRequest)
        {
            try
            {
                return Ok(await _petsUseCase.RemoveArtifact(removeArtifactRequest));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }

        /// <summary>
        /// Уложить питомца спать
        /// </summary>
        /// <returns></returns>
        /// <param name="petId">Id питомца</param>
        /// <response code="500">Ошибка API</response>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Питомец уже выспался</response>

        [HttpPost("sleep")]
        public async Task<ActionResult<DateTime>> Sleep([FromBody] long petId)
        {
            try
            {
                return await _petsUseCase.Sleep(petId);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка на сервере, попробуйте позже");
            }
        }
    }
}