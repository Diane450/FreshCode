using FreshCode.DbModels;
using FreshCode.Enums;
using FreshCode.Services;

namespace FreshCode.Extensions
{
    public static class PetExtension
    {
        public static PetBonusManagerService _petBonusManager;

        //TODO: обновить среднюю силу питомца
        public static void AssignArtifact(this Pet pet, Artifact artifact)
        {
            switch (artifact.ArtifactType.Type)
            {
                case "Шапка":
                    pet.HatId = artifact.Id;
                    EquipArtifact(pet, artifact, pet.Hat);
                    break;

                case "Аксессуар":
                    pet.AccessoryId = artifact.Id;
                    EquipArtifact(pet, artifact, pet.Accessory);
                    break;
            }
        }

        private static void EquipArtifact(Pet pet, Artifact newArtifact, Artifact? currentArtifact)
        {
            if (currentArtifact is not null)
            {
                pet.RemoveBonuses(currentArtifact);
            }
            pet.SetBonuses(newArtifact.ArtifactBonuses.Select(ab => ab.Bonus).ToList());
        }

        public static void RemoveArtifact(this Pet pet, Artifact artifact)
        {
            switch (artifact.ArtifactType.Type)
            {
                case "Шапка":
                    RemoveBonuses(pet, pet.Hat);
                    pet.Hat = null;
                    break;

                case "Аксессуар":
                    RemoveBonuses(pet, pet.Accessory);
                    pet.Accessory = null;
                    break;
            }
        }

        public static void RemoveBonuses(this Pet pet, Artifact artifact)
        {
            foreach (var artifactbonus in artifact.ArtifactBonuses)
            {
                CharacteristicType characteristic = Enum.Parse<CharacteristicType>(artifactbonus.Bonus.Characteristic.Characteristic1, true);
                var bonusValue = artifactbonus.Bonus.Value;
                var bonusType = artifactbonus.Bonus.Type.Type == "flat" ? Enums.BonusType.Flat : Enums.BonusType.Percentage;

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

        private static void SetBonuses(this Pet pet, List<Bonu> bonuses)
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

        private static int RemoveBonus(int stat, int value, Enums.BonusType type)
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

        private static int ApplyBonus(int stat, int value, Enums.BonusType type)
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
    }
}
