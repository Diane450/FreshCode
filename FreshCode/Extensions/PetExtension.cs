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
                    CheckStatIsValid(pet.CurrentHealth, pet.Level.MaxHealth);
                    pet.CurrentHealth = (int)(pet.CurrentHealth * pet.Level.EnhancementCoefficient) > pet.Level.MaxHealth ? pet.Level.MaxHealth : (int)(pet.CurrentHealth * pet.Level.EnhancementCoefficient);
                    break;
                case CharacteristicType.Defence:
                    CheckStatIsValid(pet.CurrentDefence, pet.Level.MaxDefence);
                    pet.CurrentDefence = (int)(pet.CurrentDefence * pet.Level.EnhancementCoefficient) > pet.Level.MaxDefence ? pet.Level.MaxDefence : (int)(pet.CurrentDefence * pet.Level.EnhancementCoefficient);
                    break;
                case CharacteristicType.Strength:
                    CheckStatIsValid(pet.CurrentStrength, pet.Level.MaxStrength);
                    pet.CurrentStrength = (int)(pet.CurrentStrength * pet.Level.EnhancementCoefficient) > pet.Level.MaxStrength ? pet.Level.MaxStrength : (int)(pet.CurrentStrength * pet.Level.EnhancementCoefficient);
                    break;
                case CharacteristicType.CriticalDamage:
                    CheckStatIsValid(pet.CurrentCriticalDamage, pet.Level.MaxCriticalDamage);
                    pet.CurrentCriticalDamage = (int)(pet.CurrentCriticalDamage * pet.Level.EnhancementCoefficient) > pet.Level.MaxCriticalDamage ? pet.Level.MaxCriticalDamage : (int)(pet.CurrentCriticalDamage * pet.Level.EnhancementCoefficient);
                    break;
                case CharacteristicType.CriticalChance:
                    CheckStatIsValid(pet.CurrentCriticalChance, pet.Level.MaxCriticalChance);
                    pet.CurrentCriticalChance = (int)(pet.CurrentCriticalChance * pet.Level.EnhancementCoefficient) > pet.Level.MaxCriticalChance ? pet.Level.MaxCriticalChance : (int)(pet.CurrentCriticalChance * pet.Level.EnhancementCoefficient);
                    break;
            }
        }

        private static void CheckStatIsValid(int currentValue, int maxValue)
        {
            if (currentValue >= maxValue)
            {
                throw new ArgumentException("Достигнуто максимальное значение");
            }
        }

        private static void CheckStatIsValid(decimal currentValue, int maxValue)
        {
            if (currentValue >= maxValue)
            {
                throw new ArgumentException("Достигнуто максимальное значение");
            }
        }

    }
}
