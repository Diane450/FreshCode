using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class TaskMapper
    {
        public static TaskDTO ToDTO(UserTask userTask)
        {
            return new TaskDTO
            {
                Id = userTask.Task.Id,
                Descryption = userTask.Task.Descryption,
                MoneyReward = userTask.Task.MoneyReward,
                PointsReward = userTask.Task.PointsReward,
                StatPointsReward = userTask.Task.StatPointsReward,
                PrimogemsReward = userTask.Task.PrimogemsReward,
                IsCompleted = userTask.CompletedAt != null,
                IsRewardReceived = userTask.IsRewardReceived
            };
        }
    }
}
