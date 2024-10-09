using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("feed-task")]
        public async Task<bool?> IsFeedingTaskComplete()
        {
            var userId = GetUserId(HttpContext);
            return await _taskUseCase.IsFeedingTaskComplete(userId);
        }
    }
}
