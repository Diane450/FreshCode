using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface ITaskRepository
    {
        public IQueryable<UserTask> GetTasks();
    }
}
