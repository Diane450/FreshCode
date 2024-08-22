using FreshCode.DbModels;
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
            pet.SetArtifactBonuses(artifactDTO);
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

        public static void SetArtifactBonuses(this Pet pet, ArtifactDTO artifact)
        {
            foreach (var bonus in artifact.Bonuses)
            {
                var characteristic = bonus.Characteristic;
                var bonusValue = bonus.Value;
                var bonusType = bonus.Type;

                switch (characteristic)
                {
                    case ("Критический урон"):
                        pet.CurrentCriticalDamage += bonusValue;
                        break;
                    case ("Защита"):
                        pet.CurrentDefence = RemoveBonus(pet.CurrentDefence, bonusValue, bonusType);
                        break;
                    case ("Критический шанс"):
                        pet.CurrentCriticalChance += bonusValue;
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
    }
}
