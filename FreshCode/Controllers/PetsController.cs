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
        /// ������ ������ � ������� ������������
        /// </summary>
        /// <returns></returns>
        /// <response code="200">�������� ����������</response>
        /// <response code="400">� ������������ ��� �������</response>
        /// <response code="500">������ API</response>

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
                return StatusCode(500,"������ �� �������, ���������� �����");
            }
        }

        /// <summary>
        /// ��������� ���������� �� ���������� �������
        /// </summary>
        /// <returns></returns>
        /// <param name="petId">Id �������</param>
        /// <response code="200">�������� ����������</response>
        /// <response code="400">� ������������ ��� �������</response>
        /// <response code="500">������ API</response>

        [HttpGet("artifacts")]
        public async Task<List<ArtifactDTO>> GetPetArtifacts([FromBody] long petId)
        {
            return await _petsUseCase.GetPetArtifacts(petId);
        }

        /// <summary>
        /// ��������� ���������� � ������ ������� ������ � ��������
        /// </summary>
        /// <returns></returns>
        /// <param name="petId">Id �������</param>

        [HttpGet("total-stats")]
        public async Task<PetStatResponse> GetPetStats([FromBody] long petId)
        {
            return await _petsUseCase.GetPetStats(petId);
        }

        /// <summary>
        /// ����������(���������) ������ �������
        /// </summary>
        /// <returns></returns>
        /// <param name="petId">Id �������</param>

        [HttpPut("levelup")]
        public async Task<PetDTO> LevelUp(long petId)
        {
            return await _petsUseCase.LevelUpAsync(petId);
        }

        /// <summary>
        /// ����������(���������) ������ �������
        /// </summary>
        /// <returns></returns>
        /// <param name="petId">Id �������</param>
        /// <response code="200">�������� ����������</response>
        /// <response code="409">�������� �������(����� ��� ��������� ������)</response>
        /// <response code="400">������������ ��� ������� �� �������</response>
        /// <response code="500">������ API</response>
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
                return StatusCode(500, "��������� ������ �� �������, ���������� �����");
            }

        }

        /// <summary>
        /// �������� ������ �������
        /// </summary>
        /// <returns></returns>
        /// <param name="request">������ �� �������� �������</param>
        /// <response code="500">������ API</response>
        /// <response code="200">�������� ����������</response>

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
                return StatusCode(500, "��������� ������ �� �������, ���������� �����");
            }
        }

        /// <summary>
        /// ������� ������ �������
        /// </summary>
        /// <returns></returns>
        /// <param name="request">������ �� ���������</param>
        /// <response code="500">������ API</response>
        /// <response code="200">�������� ����������</response>
        /// <response code="400">������������ ��� ��� ������� ������</response>

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
                return StatusCode(500, "��������� ������ �� �������, ���������� �����");
            }
        }

        /// <summary>
        /// ��������� ����� ��������
        /// </summary>
        /// <returns></returns>
        /// <param name="setArtifactRequest">������ �� ���������� ����� ��������</param>
        /// <response code="500">������ API</response>
        /// <response code="200">�������� ����������</response>

        [HttpPut("set-artifact")]
        public async Task<ActionResult<PetDTO>> SetArtifact([FromBody] SetArtifactRequest setArtifactRequest)
        {
            try
            {
                return Ok(await _petsUseCase.SetArtifact(setArtifactRequest));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "��������� ������ �� �������, ���������� �����");
            }
        }
        /// <summary>
        /// ��������� ����� ��������
        /// </summary>
        /// <returns></returns>
        /// <param name="removeArtifactRequest">������ �� ������ ���������</param>
        /// <response code="500">������ API</response>
        /// <response code="200">�������� ����������</response>

        [HttpPut("remove-artifact")]
        public async Task<ActionResult<PetDTO>> RemoveArtifact([FromBody] RemoveArtifactRequest removeArtifactRequest)
        {
            try
            {
                return Ok(await _petsUseCase.RemoveArtifact(removeArtifactRequest));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "��������� ������ �� �������, ���������� �����");
            }
        }

        /// <summary>
        /// ������� ������� �����
        /// </summary>
        /// <returns></returns>
        /// <param name="petId">Id �������</param>
        /// <response code="500">������ API</response>
        /// <response code="200">�������� ����������</response>
        /// <response code="400">������� ��� ��������</response>

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
                return StatusCode(500, "��������� ������ �� �������, ���������� �����");
            }
        }
    }
}