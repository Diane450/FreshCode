using FreshCode.DbModels;
using FreshCode.Enums;
using FreshCode.Interfaces;
using FreshCode.Services;

namespace FreshCode.Extensions
{
    public static class PetExtension
    {
        public static void IncreaseStat(this Pet pet, CharacteristicType CharacteristicType)
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

        private static void CheckStatIsValid(int currentValue, int maxValue)
        {
            if (currentValue > maxValue)
            {
                throw new ArgumentException("Достигнуто максимальное значение");
            }
        }
    }
}
