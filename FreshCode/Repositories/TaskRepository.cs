using FreshCode.DbModels;
using FreshCode.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class TaskRepository(FreshCodeContext dbContext) : ITaskRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public IQueryable<UserTask> GetTasks()
        {
            return _dbContext.UserTasks;
        }
    }
}
