using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class TaskMapper
    {
        public static TaskDTO ToDTO(DbModels.Task task)
        {
            var userTask = task.UserTasks.FirstOrDefault();

            return new TaskDTO
            {
                Id = task.Id,
                Descryption = task.Descryption,
                MoneyReward = task.MoneyReward,
                PointsReward = task.PointsReward,
                StatPointsReward = task.StatPointsReward,
                PrimogemsReward = task.PrimogemsReward,
                IsCompleted = userTask.IsCompleted
            };
        }
    }
}
