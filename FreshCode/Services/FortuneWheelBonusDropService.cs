using FreshCode.DbModels;
using FreshCode.Responses;

namespace FreshCode.Services
{
    public class FortuneWheelBonusDropService
    {
        public FortuneWheelDropResponse GetRandomBonus(IQueryable<Bonu> bonuses)
        {
            Random random = new Random();

            var bonusList = bonuses.ToList();
            Bonu bonu = bonusList[random.Next(bonusList.Count)];

            return new FortuneWheelDropResponse
            {
                Characteristic = 
            }
        }
    }
}
