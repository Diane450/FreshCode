using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Responses;

namespace FreshCode.UseCases
{
    public class TaskUseCase(ITaskRepository taskRepository,
        IUserRepository userRepository,
        IPetsRepository petRepository,
        IBaseRepository baseRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPetsRepository _petRepository = petRepository;
        private readonly IBaseRepository _baseRepository = baseRepository;

        public async Task<bool?> IsFeedingTaskComplete(long userId)
        {
            return null;
        }

        public async Task<TaskRewardResponse> GetReward(long taskId, long userId)
        {
            UserTask task = _userRepository.GetUserTasks(userId)
                .Where(ut => ut.TaskId == taskId && ut.CreatedAt.Date == DateTime.UtcNow.Date)
                .FirstOrDefault();

            if (task == null)
            {
                throw new Exception("У пользователя нет задания");
            }

            if (task.IsRewardReceived)
                throw new Exception("Пользователь уже получил награду.");

            if (task.CompletedAt == null)
                throw new Exception("Задание не выполнено");

            User user = await _userRepository.GetUserById(userId);
            Pet pet = await _petRepository.GetPetByUserId(userId);

            user.PrimogemsCount += task.Task.PrimogemsReward;
            user.StatPoints += task.Task.StatPointsReward;
            user.Money += task.Task.MoneyReward;
            pet.Points += task.Task.PointsReward;

            task.IsRewardReceived = true;
            await _baseRepository.SaveChangesAsync();

            return new TaskRewardResponse
            {
                PrimogemsCount = user.PrimogemsCount,
                StatPoints = user.StatPoints,
                Money = user.Money,
                Points = pet.Points
            };
        }
    }
}
