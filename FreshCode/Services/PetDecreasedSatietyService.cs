
using FreshCode.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace FreshCode.Services
{
    public class PetDecreasedSatietyService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public PetDecreasedSatietyService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UpdateSatietyLevels(cancellationToken);
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }

        private async System.Threading.Tasks.Task UpdateSatietyLevels(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FreshCodeContext>();

                var pets = await dbContext.Pets
                            .Include(p => p.PetFeedLogs)
                            .ToListAsync(cancellationToken);

                foreach (var pet in pets)
                {
                    // Получаем последний лог кормления
                    var feedLog = pet.PetFeedLogs
                                    .OrderByDescending(p => p.CreatedAt)
                                    .FirstOrDefault();

                    // Определяем время, прошедшее с момента последнего кормления
                    TimeSpan timeDifference;
                    if (feedLog == null)
                    {
                        // Если питомца никогда не кормили, считаем от даты создания
                        timeDifference = DateTime.UtcNow - pet.CreatedAt;
                    }
                    else
                    {
                        // Если есть лог кормления, считаем от последнего кормления
                        timeDifference = DateTime.UtcNow - feedLog.CreatedAt;
                    }

                    double seconds = timeDifference.TotalSeconds;

                    // Потребность в еде уменьшается в зависимости от времени, прошедшего с последнего кормления
                    var feedNeedDecrease = seconds / (5 * 60 * 60); // Процент уменьшения каждые 5 часов

                    // Новое значение потребности в еде
                    var newFeedValue = Math.Max(0, pet.FeedNeed - feedNeedDecrease); // Убедимся, что значение не меньше 0

                    // Если значение изменилось, обновляем БД
                    if (newFeedValue != pet.FeedNeed)
                    {
                        pet.FeedNeed = (int)newFeedValue;
                        await dbContext.SaveChangesAsync(cancellationToken);
                    }
                }
            }
        }
    }
}