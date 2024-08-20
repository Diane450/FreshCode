using FreshCode.DbModels;

namespace FreshCode.ModelsDTO
{
    public class ArtifactDTO
    {
        public long Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Price { get; set; }

        public string Rarity { get; set; } = null!;

        public List<BonusDTO> Bonuses { get; set; } = null!;

        public string Type { get; set; } = null!;
    }
    public static class PetExtension
    {
        //TODO: обновить среднюю силу питомца
        public static void ChangeArtifact(this Pet pet, ArtifactDTO artifactDTO)
        {
            switch (artifactDTO.Type)
            {
                case "Шапка":
                    if (pet.Hat is null)
                    {
                        pet.HatId = artifactDTO.Id;
                        return;
                    }
                    pet.RemoveArtifact(pet.Hat);
                    pet.SetArtifact(artifactDTO);
                    pet.HatId = artifactDTO.Id;

                    break;

                case "Аксессуар":
                    if (pet.Accessory is null)
                    {
                        pet.AccessoryId = artifactDTO.Id;
                        return;
                    }
                    pet.RemoveArtifact(pet.Accessory);
                    pet.SetArtifact(artifactDTO);
                    pet.AccessoryId = artifactDTO.Id;
                    break;

            }
        }

        public static void RemoveArtifact(this Pet pet, Artifact artifact)
        {
            foreach (var artifactbonus in artifact.ArtifactBonuses)
            {
                switch (artifactbonus.Bonus.Characteristic.Characteristic1)
                {
                    case ("Критический урон"):
                        pet.CurrentCriticalDamage -= artifactbonus.Bonus.Value;
                        break;
                    case ("Защита"):
                        if (artifactbonus.Bonus.Type.Type == "flat")
                        {
                            pet.CurrentDefence -= artifactbonus.Bonus.Value;
                        }
                        else
                        {
                            pet.CurrentDefence -= (pet.CurrentDefence / 100) * artifactbonus.Bonus.Value;
                        }
                        break;
                    case ("Критический шанс"):
                        pet.CurrentCriticalChance -= artifactbonus.Bonus.Value;
                        break;
                    case ("Здоровье"):
                        if (artifactbonus.Bonus.Type.Type == "flat")
                        {
                            pet.CurrentHealth -= artifactbonus.Bonus.Value;
                        }
                        else
                        {
                            pet.CurrentHealth -= (pet.CurrentHealth / 100) * artifactbonus.Bonus.Value;
                        }
                        pet.CurrentHealth -= artifactbonus.Bonus.Value;
                        break;
                    case ("Сила"):
                        if (artifactbonus.Bonus.Type.Type == "flat")
                        {
                            pet.CurrentStrength -= artifactbonus.Bonus.Value;
                        }
                        else
                        {
                            pet.CurrentStrength -= (pet.CurrentStrength / 100) * artifactbonus.Bonus.Value;
                        }
                        pet.CurrentStrength -= artifactbonus.Bonus.Value;
                        break;
                }
            }
        }

        public static void SetArtifact(this Pet pet, ArtifactDTO artifact)
        {
            foreach (var bonus in artifact.Bonuses)
            {
                switch (bonus.Characteristic)
                {
                    case ("Критический урон"):
                        pet.CurrentCriticalDamage += bonus.Value;
                        break;
                    case ("Защита"):
                        if (bonus.Type == "flat")
                        {
                            pet.CurrentDefence += bonus.Value;
                        }
                        else
                        {
                            pet.CurrentDefence += (pet.CurrentDefence / 100) * bonus.Value;
                        }
                        break;
                    case ("Критический шанс"):
                        pet.CurrentCriticalChance += bonus.Value;
                        break;
                    case ("Здоровье"):
                        if (bonus.Type == "flat")
                        {
                            pet.CurrentHealth += bonus.Value;
                        }
                        else
                        {
                            pet.CurrentHealth += (pet.CurrentHealth / 100) * bonus.Value;
                        }
                        pet.CurrentHealth += bonus.Value;
                        break;
                    case ("Сила"):
                        if (bonus.Type == "flat")
                        {
                            pet.CurrentStrength += bonus.Value;
                        }
                        else
                        {
                            pet.CurrentStrength += (pet.CurrentStrength / 100) * bonus.Value;
                        }
                        pet.CurrentStrength += bonus.Value;
                        break;
                }
            }
        }
    }
}
