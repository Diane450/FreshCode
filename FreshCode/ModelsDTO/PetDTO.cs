using FreshCode.DbModels;

namespace FreshCode.ModelsDTO
{
    public class PetDTO
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public long UserId { get; set; }

        public BodyDTO Body { get; set; } = null!;

        public EyeDTO Eyes { get; set; } = null!;

        public ArtifactDTO? Hat { get; set; } = null!;

        public ArtifactDTO? Accessory { get; set; } = null!;

        public int SleepNeed { get; set; }

        public int FeedNeed { get; set; }

        public int FightNeed { get; set; }

        public decimal GeneralHappiness { get; set; }

        public long Level { get; set; }

        public int Points { get; set; }

        public int MaxPoints { get; set; }

        public int CurrentHealth { get; set; }

        public int CurrentStrength { get; set; }

        public int CurrentDefence { get; set; }

        public decimal CurrentCriticalDamage { get; set; }

        public decimal CurrentCriticalChance { get; set; }

        public int MaxHealth { get; set; }

        public int MaxStrength { get; set; }

        public int MaxDefence { get; set; }

        public decimal MaxCriticalDamage { get; set; }

        public decimal MaxCriticalChance { get; set; }

        public decimal AveragePower { get; set; }
    }
}
