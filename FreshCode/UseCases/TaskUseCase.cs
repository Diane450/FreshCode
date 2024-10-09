using FreshCode.DbModels;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.UseCases
{
    public class TaskUseCase(FreshCodeContext dbContext)
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async Task<bool?> IsFeedingTaskComplete(long userId)
        {
            var userTask = await _dbContext.UserTasks
                .FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TaskId == 1);

            if(userTask is null)
                return null;

            bool isFeedingTaskCompleted = userTask.IsCompleted;

            return !isFeedingTaskCompleted;
        }
    }
}
