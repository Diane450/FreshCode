using FreshCode.DbModels;
using FreshCode.Enums;
using FreshCode.Interfaces;
using FreshCode.Responses;

namespace FreshCode.Services
{
    public class PetBonusManagerService(IPetLoggerService petLoggerService) : IPetBonusManagerService
    {
        private readonly IPetLoggerService _petLoggerService = petLoggerService;

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
                        var newSleepValue = ApplyBonus(pet.SleepNeed, bonusValue, bonusType);
                        pet.SleepNeed = newSleepValue <= 100 ? newSleepValue : 100;                        
                        break;
                    case (CharacteristicType.FeedNeed):
                        var newFeedValue = ApplyBonus(pet.FeedNeed, bonusValue, bonusType);
                        pet.FeedNeed = newFeedValue <=100 ? newFeedValue : 100;
                        break;
                    default:
                        throw new Exception($"Некорректная характеристика");
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
                stat += Convert.ToInt32(Math.Ceiling(((double)stat / 100) * value));
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

        public PetStatResponse GetBonuses(PetStatResponse petResponse, List<Bonu> bonuses)
        {
            foreach (var bonus in bonuses)
            {
                CharacteristicType characteristic = Enum.Parse<CharacteristicType>(bonus.Characteristic.Characteristic1, true);
                var bonusValue = bonus.Value;
                var bonusType = bonus.Type.Type == "flat" ? Enums.BonusType.Flat : Enums.BonusType.Percentage;
                switch (characteristic)
                {
                    case (CharacteristicType.CriticalDamage):
                        petResponse.CriticalDamage += bonusValue;
                        break;
                    case (CharacteristicType.Defence):
                        petResponse.Defence = ApplyBonus(petResponse.Defence, bonusValue, bonusType);
                        break;
                    case (CharacteristicType.CriticalChance):
                        petResponse.CriticalChance+= bonusValue;
                        break;
                    case (CharacteristicType.Health):
                        petResponse.Health = ApplyBonus(petResponse.Health, bonusValue, bonusType);
                        break;
                    case (CharacteristicType.Strength):
                        petResponse.Strength = ApplyBonus(petResponse.Strength, bonusValue, bonusType);
                        break;
                    case (CharacteristicType.SleepNeed):
                        var newSleepValue = ApplyBonus(petResponse.SleepNeed, bonusValue, bonusType);
                        petResponse.SleepNeed = newSleepValue <= 100 ? newSleepValue : 100;
                        break;
                    case (CharacteristicType.FeedNeed):
                        var newFeedValue = ApplyBonus(petResponse.FeedNeed, bonusValue, bonusType);
                        petResponse.FeedNeed = newFeedValue <= 100 ? newFeedValue : 100;
                        break;
                    default:
                        throw new Exception($"Invalid CharacteristicType {characteristic}");
                }
            }
            return petResponse;
        }

        public async System.Threading.Tasks.Task SetBonus(Pet pet, Bonu bonus)
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
                    var newSleepValue = ApplyBonus(pet.SleepNeed, bonusValue, bonusType);
                    pet.SleepNeed = newSleepValue <= 100 ? newSleepValue : 100;
                    await _petLoggerService.CreateSleepLog(pet);
                    break;
                case (CharacteristicType.FeedNeed):
                    var newFeedValue = ApplyBonus(pet.FeedNeed, bonusValue, bonusType);
                    pet.FeedNeed = newFeedValue <= 100 ? newFeedValue : 100;
                    await _petLoggerService.CreateFeedLog(pet);
                    break;
                default:
                    throw new Exception($"Invalid CharacteristicType {characteristic}");
            }
        }
    }
}
