using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class PetMapper
    {
        public static PetDTO ToDto(Pet pet)
        {
            return new PetDTO
            {
                Id = pet.Id,
                Name = pet.Name,
                UserId = pet.UserId,
                Body = BodyMapper.ToDTO(pet.Body),
                Eyes = EyeMapper.ToDTO(pet.Eyes),
                Hat = ArtifactMapper.ToDTO(pet.Hat),
                Accessory = ArtifactMapper.ToDTO(pet.Accessory),
                SleepNeed = pet.SleepNeed,
                FeedNeed = pet.FeedNeed,
                FightNeed = pet.FightNeed,
                GeneralHappiness = pet.GeneralHappiness,
                Level = pet.Level,
                Points = pet.Points,
                MaxPoints = pet.MaxPoints,
                CurrentHealth = pet.CurrentHealth,
                CurrentStrength = pet.CurrentStrength,
                CurrentDefence = pet.CurrentDefence,
                CurrentCriticalDamage = pet.CurrentCriticalDamage,
                CurrentCriticalChance = pet.CurrentCriticalChance,
                MaxHealth = pet.MaxHealth,
                MaxStrength = pet.MaxStrength,
                MaxDefence = pet.MaxDefence,
                MaxCriticalDamage = pet.MaxCriticalDamage,
                MaxCriticalChance = pet.MaxCriticalChance,
                AveragePower = pet.AveragePower
            };
        }

        public static Pet ToEntity(PetDTO pet)
        {
            return new Pet
            {
                Id = pet.Id,
                Name = pet.Name,
                UserId = pet.UserId,
                BodyId = pet.Body.Id,
                EyesId = pet.Eyes.Id,
                HatId = pet.Hat?.Id,
                AccessoryId = pet.Accessory?.Id,
                SleepNeed = pet.SleepNeed,
                FeedNeed = pet.FeedNeed,
                FightNeed = pet.FightNeed,
                GeneralHappiness = pet.GeneralHappiness,
                Level = pet.Level,
                Points = pet.Points,
                MaxPoints = pet.MaxPoints,
                CurrentHealth = pet.CurrentHealth,
                CurrentStrength = pet.CurrentStrength,
                CurrentCriticalChance = pet.CurrentCriticalChance,
                CurrentCriticalDamage = pet.CurrentCriticalDamage,
                CurrentDefence = pet.CurrentDefence,

            };
        }
    }
}
