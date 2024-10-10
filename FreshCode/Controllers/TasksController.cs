using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Responses;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TasksController : BaseController
    {
        private readonly TaskUseCase _taskUseCase;

        public TasksController(TaskUseCase taskUseCase)
        {
            _taskUseCase = taskUseCase;
        }

        [HttpGet("reward")]
        public async Task<TaskRewardResponse> GetTaskReward([FromBody] long taskId)
        {
            var userId = GetUserId(HttpContext);
            return await _taskUseCase.GetReward(taskId, userId);
        }
    }
}
