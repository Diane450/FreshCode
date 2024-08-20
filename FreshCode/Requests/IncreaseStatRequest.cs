using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Requests
{
    public class IncreaseStatRequest
    {
        public PetDTO PetDTO { get; set; } = null!;
        public CharacteristicType Characteristic { get; set; }
    }
    public enum CharacteristicType
    {
        Health,
        Strength,
        Defence,
        CriticalDamage,
        CriticalChance
    }

    public static class CharacteristicTypeExtensions
    {
        public static void IncreaseStat(this CharacteristicType CharacteristicType, Pet pet)
        {
            switch (CharacteristicType)
            {
                case CharacteristicType.Health:
                    pet.CurrentHealth = (int)(pet.CurrentHealth * pet.Level.EnhancementCoefficient);
                    CheckStatIsValid(pet.CurrentHealth, pet.Level.MaxHealth);
                    break;
                case CharacteristicType.Defence:
                    pet.CurrentDefence = (int)(pet.CurrentDefence * pet.Level.EnhancementCoefficient);
                    break;
                case CharacteristicType.Strength:
                    pet.CurrentStrength = (int)(pet.CurrentStrength * pet.Level.EnhancementCoefficient);
                    break;
                case CharacteristicType.CriticalDamage:
                    pet.CurrentCriticalDamage = (int)(pet.CurrentCriticalDamage * pet.Level.EnhancementCoefficient);
                    break;
                case CharacteristicType.CriticalChance:
                    pet.CurrentCriticalChance = (int)(pet.CurrentCriticalChance * pet.Level.EnhancementCoefficient);
                    break;
            }
        }

        public static void CheckStatIsValid(int currentValue, int maxValue)
        {
            if (currentValue > maxValue)
            {
                throw new ArgumentException("Достигнуто максимальное значение");
            }
        }
    }
}
