namespace FreshCode.ModelsDTO
{
    public class PetBattleDTO
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

        public int CurrentHealth { get; set; }

        public int CurrentStrength { get; set; }

        public int CurrentDefence { get; set; }

        public decimal CurrentCriticalDamage { get; set; }

        public decimal CurrentCriticalChance { get; set; }

        public int MaxHealth { get; set; }
    }
}
