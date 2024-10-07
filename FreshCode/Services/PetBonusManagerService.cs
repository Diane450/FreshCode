using FreshCode.DbModels;
using FreshCode.Enums;
using FreshCode.Interfaces;

namespace FreshCode.Services
{
    public class PetBonusManagerService : IPetBonusManagerService
    {
        public void SetBonuses(Pet pet, List<Bonu> bonuses)
        {
            foreach (var bonus in bonuses)
            {
                CharacteristicType characteristic = Enum.Parse<CharacteristicType>(bonus.Characteristic.Characteristic1, true);
                var bonusValue = bonus.Value;
                var bonusType = bonus.Type.Type == "flat" ? Enums.BonusType.Flat : Enums.BonusType.Percentage;
                switch (characteristic)
                {
                    case (CharacteristicType.CriticalDamage):
                        pet.CurrentCriticalDamage += bonusValue;
                        break;
                    case (CharacteristicType.Defence):
                        pet.CurrentDefence = ApplyBonus(pet.CurrentDefence, bonusValue, bonusType);
                        break;
                    case (CharacteristicType.CriticalChance):
                        pet.CurrentCriticalChance += bonusValue;
                        break;
                    case (CharacteristicType.Health):
                        pet.CurrentHealth = ApplyBonus(pet.CurrentHealth, bonusValue, bonusType);
                        break;
                    case (CharacteristicType.Strength):
                        pet.CurrentStrength = ApplyBonus(pet.CurrentStrength, bonusValue, bonusType);
                        break;
                    case (CharacteristicType.SleepNeed):
                        pet.SleepNeed = ApplyBonus(pet.SleepNeed, bonusValue, bonusType);
                        break;
                    case (CharacteristicType.FeedNeed):
                        pet.FeedNeed = ApplyBonus(pet.FeedNeed, bonusValue, bonusType);
                        break;
                    default:
                        throw new Exception($"Invalid CharacteristicType {characteristic}");
                }
            }

        }
        
        private int ApplyBonus(int stat, int value, Enums.BonusType type)
        {
            if (type == Enums.BonusType.Flat)
            {
                stat += value;
            }
            else
            {
                stat += (stat / 100) * value;
            }
            return stat;
        }

        public void RemoveBonuses(Pet pet, List<Bonu> bonuses)
        {
            foreach (var bonus in bonuses)
            {
                CharacteristicType characteristic = Enum.Parse<CharacteristicType>(bonus.Characteristic.Characteristic1, true);
                var bonusValue = bonus.Value;
                var bonusType = bonus.Type.Type == "flat" ? Enums.BonusType.Flat : Enums.BonusType.Percentage;

                switch (characteristic)
                {
                    case (CharacteristicType.CriticalDamage):
                        pet.CurrentCriticalDamage -= bonusValue;
                        break;
                    case (CharacteristicType.CriticalChance):
                        pet.CurrentCriticalChance -= bonusValue;
                        break;
                    case (CharacteristicType.Defence):
                        pet.CurrentDefence = RemoveBonus(pet.CurrentDefence, bonusValue, bonusType);
                        break;
                    case (CharacteristicType.Health):
                        pet.CurrentHealth = RemoveBonus(pet.CurrentHealth, bonusValue, bonusType);
                        break;
                    case (CharacteristicType.Strength):
                        pet.CurrentStrength = RemoveBonus(pet.CurrentStrength, bonusValue, bonusType);
                        break;
                    case (CharacteristicType.SleepNeed):
                        pet.SleepNeed = RemoveBonus(pet.SleepNeed, bonusValue, bonusType);
                        break;
                    case (CharacteristicType.FeedNeed):
                        pet.FeedNeed = RemoveBonus(pet.FeedNeed, bonusValue, bonusType);
                        break;
                    default:
                        throw new Exception($"Invalid CharacteristicType {characteristic}");
                }
            }

        }
        
        private int RemoveBonus(int stat, int value, Enums.BonusType type)
        {
            if (type == Enums.BonusType.Flat)
            {
                stat -= value;
            }
            else
            {
                stat -= (stat / 100) * value;
            }
            return stat;
        }
    }
}
