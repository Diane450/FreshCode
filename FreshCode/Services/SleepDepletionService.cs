
using FreshCode.DbModels;
using FreshCode.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Services
{
    public class SleepDepletionService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        //private readonly IHubContext<SleepNotificationHub> _hubContext;

        public SleepDepletionService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }

        protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await UpdateSleepLevels(stoppingToken);
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async System.Threading.Tasks.Task UpdateSleepLevels(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FreshCodeContext>();
                var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<SleepNotificationHub>>();

                var pets = await dbContext.Pets
                    .Include(p => p.PetSleepLogs)
                    .ToListAsync(cancellationToken);

                foreach (var pet in pets)
                {
                    var sleepLog = pet.PetSleepLogs
                        .OrderByDescending(p => p.Id)
                        .FirstOrDefault();
                    if (sleepLog == null)
                    {
                        //чекаем когда питомец был создан и используем эту дату 
                    }
                    else
                    {
                        TimeSpan timeDifference = DateTime.UtcNow - sleepLog.WokeUpAt;
                        double seconds = timeDifference.TotalSeconds;

                        if (pet.SleepNeed == 0)
                        {
                            // Отправляем уведомление на фронтенд
                            await hubContext.Clients.User(pet.UserId.ToString()).SendAsync("SleepDepleted", pet.Id);
                        }

                        var newSleepValue = Convert.ToInt32(Math.Floor(100 - seconds / ((24 * 60 * 60) * 100)));

                        if (newSleepValue == 0)
                        {
                            pet.SleepNeed = newSleepValue;
                            // Отправляем уведомление на фронтенд
                            await hubContext.Clients.User(pet.UserId.ToString()).SendAsync("SleepDepleted", pet.Id);
                        }
                        else if (newSleepValue != pet.SleepNeed)
                        {
                            pet.SleepNeed = newSleepValue;
                            await hubContext.Clients.User(pet.UserId.ToString()).SendAsync("SleepUpdated", pet.Id);
                        }
                    }
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}
