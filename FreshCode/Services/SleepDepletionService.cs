using FreshCode.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Services
{
    public class SleepDepletionService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

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

                var pets = await dbContext.Pets
                    .Where(p => !p.IsSleeping)
                    .Include(p => p.PetSleepLogs)
                    .ToListAsync(cancellationToken);

                foreach (var pet in pets)
                {
                    var sleepLog = pet.PetSleepLogs
                        .OrderByDescending(p => p.Id)
                        .FirstOrDefault();
                    TimeSpan timeDifference;
                    if (sleepLog == null)
                    {
                        timeDifference = DateTime.UtcNow - pet.CreatedAt;
                    }
                    else
                    {
                        timeDifference = DateTime.UtcNow - sleepLog.WokeUpAt;
                    }

                    double seconds = timeDifference.TotalSeconds;

                    var sleepLevel = sleepLog is null ? 100:sleepLog.SleepLevel ;

                    int newSleepValue = sleepLevel - (int)Math.Ceiling((seconds * 100) / 18000);

                    if (newSleepValue != pet.SleepNeed)
                    {
                        pet.SleepNeed = newSleepValue < 0 ? 0 : newSleepValue;
                        await dbContext.SaveChangesAsync(cancellationToken);
                    }
                }
            }
        }
    }
}
