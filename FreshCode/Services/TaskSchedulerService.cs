using FreshCode.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Services
{
    public class TaskSchedulerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TaskSchedulerService> _logger;

        public TaskSchedulerService(IServiceScopeFactory scopeFactory, ILogger<TaskSchedulerService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Ждём до ближайшего 00:00
                var nextDailyTaskTime = GetNextDailyTaskTime();
                var delay = nextDailyTaskTime - DateTime.Now;
                _logger.LogInformation($"Ожидание до следующего выполнения ежедневных заданий: {delay.TotalHours} часов.");
                await System.Threading.Tasks.Task.Delay(delay, stoppingToken);

                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<FreshCodeContext>();

                    // Добавляем ежедневные задания
                    await AddDailyTasks(context);

                    // Если это понедельник, добавляем еженедельные задания
                    if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
                    {
                        await AddWeeklyTasks(context);
                    }
                    await context.SaveChangesAsync();
                }
            }
        }

        private DateTime GetNextDailyTaskTime()
        {
            // Следующее 00:00
            var next = DateTime.Today.AddDays(1); // Завтра в 00:00
            return next;
        }

        private async System.Threading.Tasks.Task AddDailyTasks(FreshCodeContext context)
        {
            var tasks = await context.Tasks.Where(t => t.IsDaily == true).ToListAsync();
            var users = await context.Users.ToListAsync();

            foreach (var user in users)
            {
                foreach (var task in tasks)
                {
                    var userTask = new UserTask()
                    {
                        TaskId = task.Id,
                        UserId = user.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsRewardReceived = false
                    };
                    await context.AddAsync(userTask);
                }
            }
            _logger.LogInformation("Ежедневное задание добавлено.");
        }

        private async System.Threading.Tasks.Task AddWeeklyTasks(FreshCodeContext context)
        {
            //var currentWeek = GetIso8601WeekOfYear(DateTime.Today);

            //var taskInCurrentWeek = context.Tasks.Any(t => !t.IsDaily && GetIso8601WeekOfYear(t.CreatedAt) == currentWeek);

            var tasks = context.Tasks.Where(t => t.IsWeekly == true);
            var users = context.Users;

            foreach (var user in users)
            {
                foreach (var task in tasks)
                {
                    var userTask = new UserTask()
                    {
                        TaskId = task.Id,
                        UserId = user.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsRewardReceived = false
                    };
                    await context.AddAsync(userTask);
                }
            }
        }
        // Метод для вычисления номера недели в году
        //public static int GetIso8601WeekOfYear(DateTime time)
        //{
        //    DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
        //    if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
        //    {
        //        time = time.AddDays(3);
        //    }

        //    return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        //}
    }
}

