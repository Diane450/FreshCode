using FreshCode.DbModels;
using FreshCode.Enums;
using FreshCode.ModelsDTO;
using System.Security.Cryptography;

namespace FreshCode.Extensions
{
    public static class PetExtension
    {
        //TODO: обновить среднюю силу питомца
        public static void AssignArtifact(this Pet pet, ArtifactDTO artifactDTO)
        {
            switch (artifactDTO.Type)
            {
                case "Шапка":
                    pet.HatId = artifactDTO.Id;
                    EquipArtifact(pet, artifactDTO, pet.Hat);
                    break;

                case "Аксессуар":
                    pet.AccessoryId = artifactDTO.Id;
                    EquipArtifact(pet, artifactDTO, pet.Accessory);
                    break;
            }
        }

        private static void EquipArtifact(Pet pet, ArtifactDTO artifactDTO, Artifact? currentArtifact)
        {
            if (currentArtifact is not null)
            {
                pet.RemoveArtifactBonuses(currentArtifact);
            }
            //pet.SetBonuses();
        }

        public static void RemoveArtifact(this Pet pet, ArtifactDTO artifactDTO)
        {
            switch (artifactDTO.Type)
            {
                case "Шапка":
                    RemoveArtifactBonuses(pet, pet.Hat);
                    pet.Hat = null;
                    break;

                case "Аксессуар":
                    RemoveArtifactBonuses(pet, pet.Accessory);
                    pet.Accessory = null;
                    break;
            }
        }

        public static void RemoveArtifactBonuses(this Pet pet, Artifact artifact)
        {
            foreach (var artifactbonus in artifact.ArtifactBonuses)
            {
                var characteristic = artifactbonus.Bonus.Characteristic.Characteristic1;
                var bonusValue = artifactbonus.Bonus.Value;
                var bonusType = artifactbonus.Bonus.Type.Type == "flat" ? ModelsDTO.BonusType.Flat : ModelsDTO.BonusType.Percentage;

                switch (characteristic)
                {
                    case ("Критический урон"):
                        pet.CurrentCriticalDamage -= bonusValue;
                        break;
                    case ("Защита"):
                        pet.CurrentDefence = RemoveBonus(pet.CurrentDefence, bonusValue, bonusType);
                        break;
                    case ("Критический шанс"):
                        pet.CurrentCriticalChance -= bonusValue;
                        break;
                    case ("Здоровье"):
                        pet.CurrentHealth = RemoveBonus(pet.CurrentHealth, bonusValue, bonusType);
                        break;
                    case ("Сила"):
                        pet.CurrentStrength = RemoveBonus(pet.CurrentStrength, bonusValue, bonusType);
                        break;
                }
            }
        }

        private static void SetBonuses(this Pet pet, List<Bonu> bonuses)
        {
            foreach (var bonus in bonuses)
            {
                CharacteristicType characteristic = Enum.Parse<CharacteristicType>(bonus.Characteristic.Characteristic1, true);
                var bonusValue = bonus.Value;
                var bonusType = bonus.Type.Type == "flat" ? ModelsDTO.BonusType.Flat : ModelsDTO.BonusType.Percentage;
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

        private static int RemoveBonus(int stat, int value, ModelsDTO.BonusType type)
        {
            if (type == ModelsDTO.BonusType.Flat)
            {
                stat -= value;
            }
            else
            {
                stat -= (stat / 100) * value;
            }
            return stat;
        }

        private static int ApplyBonus(int stat, int value, ModelsDTO.BonusType type)
        {
            if (type == ModelsDTO.BonusType.Flat)
            {
                stat += value;
            }
            else
            {
                stat += (stat / 100) * value;
            }
            return stat;
        }

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

        public static void Feed(this Pet pet, List<Bonu> bonuses)
        {
            SetBonuses(pet, bonuses);
        }

        //public static void Feed(this Pet pet, FoodDTO foodDTO)
        //{
        //    foreach (var bonus in foodDTO.Bonuses)
        //    {
        //        var characteristic = bonus.Characteristic;
        //        var bonusValue = bonus.Value;
        //        var bonusType = bonus.Type;

        //        switch (characteristic)
        //        {
        //            case ("Критический урон"):
        //                pet.CurrentCriticalDamage += bonusValue;
        //                break;
        //            case ("Защита"):
        //                pet.CurrentDefence = ApplyBonus(pet.CurrentDefence, bonusValue, bonusType);
        //                break;
        //            case ("Критический шанс"):
        //                pet.CurrentCriticalChance += bonusValue;
        //                break;
        //            case ("Здоровье"):
        //                pet.CurrentHealth = ApplyBonus(pet.CurrentHealth, bonusValue, bonusType);
        //                break;
        //            case ("Сила"):
        //                pet.CurrentStrength = ApplyBonus(pet.CurrentStrength, bonusValue, bonusType);
        //                break;
        //        }
        //    }
        //}
    }
}
