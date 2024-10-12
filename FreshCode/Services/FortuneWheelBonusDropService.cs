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
            //return new FortuneWheelDropResponse
            //{
            //    BonusId = bonu.Id,
            //    Characteristic = bonu.Characteristic.Characteristic1,
            //    Value = bonu.Value,
            //    BonusType = bonu.Type.Type,
            //    CreatedAt = DateTime.UtcNow,
            //    ExpiresAt = bonu.Duration > 0 ? DateTime.UtcNow.AddMinutes(bonu.Duration) : null,
            //};
        }
    }
}
