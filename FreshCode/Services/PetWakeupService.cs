
using FreshCode.DbModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Threading;

namespace FreshCode.Services
{
    public class PetWakeupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public PetWakeupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected async override System.Threading.Tasks.Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await WakeUpPets(cancellationToken);
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }

        private async System.Threading.Tasks.Task WakeUpPets(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FreshCodeContext>();

                var pets = await dbContext.Pets
                    .Where(p => p.IsSleeping)
                    .Include(p => p.PetSleepLogs)
                    .ToListAsync() ;

                foreach (var pet in pets)
                {
                    var lastSleepLog = pet.PetSleepLogs.OrderByDescending(pl => pl.Id).FirstOrDefault();

                    var difference = (DateTime.UtcNow - lastSleepLog!.CreatedAt).TotalSeconds;

                    int addPercents = (int)Math.Ceiling((difference * 100) / 18000);

                    pet.SleepNeed = pet.SleepNeed + addPercents > 100 ? 100 : pet.SleepNeed + addPercents;

                    if (lastSleepLog.WokeUpAt <= DateTime.UtcNow || pet.SleepNeed == 100)
                    {
                        pet.IsSleeping = false;
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
