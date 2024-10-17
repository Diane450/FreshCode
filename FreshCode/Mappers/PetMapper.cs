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
                Level = pet.Level.LevelValue,
                Points = pet.Points,
                MaxPoints = pet.MaxPoints,
                CurrentHealth = pet.CurrentHealth,
                CurrentStrength = pet.CurrentStrength,
                CurrentDefence = pet.CurrentDefence,
                CurrentCriticalDamage = pet.CurrentCriticalDamage,
                CurrentCriticalChance = pet.CurrentCriticalChance,
                MaxHealth = pet.Level.MaxHealth,
                MaxStrength = pet.Level.MaxStrength,
                MaxDefence = pet.Level.MaxHealth,
                MaxCriticalDamage = pet.Level.MaxHealth,
                MaxCriticalChance = pet.Level.MaxHealth,
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
                LevelId = pet.Level,
                Points = pet.Points,
                MaxPoints = pet.MaxPoints,
                CurrentHealth = pet.CurrentHealth,
                CurrentStrength = pet.CurrentStrength,
                CurrentCriticalChance = pet.CurrentCriticalChance,
                CurrentCriticalDamage = pet.CurrentCriticalDamage,
                CurrentDefence = pet.CurrentDefence,

            };
        }

        public static PetBattleDTO ToBattleDto(Pet pet)
        {
            return new PetBattleDTO
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
                Level = pet.Level.LevelValue,
                CurrentHealth = pet.CurrentHealth,
                CurrentStrength = pet.CurrentStrength,
                CurrentDefence = pet.CurrentDefence,
                CurrentCriticalDamage = pet.CurrentCriticalDamage,
                CurrentCriticalChance = pet.CurrentCriticalChance,
                MaxHealth = pet.Level.MaxHealth
            };
        }
    }
}
