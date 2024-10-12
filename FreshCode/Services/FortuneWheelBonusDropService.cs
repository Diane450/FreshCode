using FreshCode.DbModels;
using FreshCode.Enums;
using FreshCode.Responses;

namespace FreshCode.Services
{
    public class FortuneWheelBonusDropService
    {
        public Bonu GetRandomBonus(IQueryable<Bonu> bonuses)
        {
            Random random = new Random();

            var bonusList = bonuses.ToList();
            Bonu bonu = bonusList[random.Next(bonusList.Count)];
            return bonu;
        }
    }
}
