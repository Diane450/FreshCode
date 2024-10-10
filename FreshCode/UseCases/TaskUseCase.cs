using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.UseCases
{
    public class TaskUseCase(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<bool?> IsFeedingTaskComplete(long userId)
        {
            return null;
        }
    }
}
