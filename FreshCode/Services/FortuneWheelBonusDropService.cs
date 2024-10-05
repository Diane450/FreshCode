using FreshCode.DbModels;
using FreshCode.Enums;
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
                Characteristic = (CharacteristicType)Enum.Parse(typeof(CharacteristicType), bonu.Characteristic.Characteristic1, true),
                Value = bonu.Value,
                BonusType = (Enums.BonusType)Enum.Parse(typeof(CharacteristicType), bonu.Characteristic.Characteristic1, true)
            };
        }
    }
}
