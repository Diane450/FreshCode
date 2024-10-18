using FreshCode.DbModels;

namespace FreshCode.ModelsDTO
{
    /// <summary>
    /// DTO питомец
    /// </summary>
    public class PetDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// внутренний Id пользователя-хозяина
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// BodyDTO Тело
        /// </summary>
        public BodyDTO Body { get; set; } = null!;
        /// <summary>
        /// EyeDTO Глаза
        /// </summary>
        public EyeDTO Eyes { get; set; } = null!;
        /// <summary>
        /// ArtifactDTO шапка
        /// </summary>
        public ArtifactDTO? Hat { get; set; } = null!;
        /// <summary>
        /// ArtifactDTO аксессуар
        /// </summary>
        public ArtifactDTO? Accessory { get; set; } = null!;
        /// <summary>
        /// Уровень сна
        /// </summary>
        public int SleepNeed { get; set; }
        /// <summary>
        /// ровень питания
        /// </summary>
        public int FeedNeed { get; set; }
        /// <summary>
        /// уровень желания подраться
        /// </summary>
        public int FightNeed { get; set; }
        /// <summary>
        /// общий уровень счастья
        /// </summary>
        public decimal GeneralHappiness { get; set; }
        /// <summary>
        /// Уровень
        /// </summary>
        public long Level { get; set; }
        /// <summary>
        /// кол-во опыта
        /// </summary>
        public int Points { get; set; }
        /// <summary>
        /// максимальное кол-во опыта
        /// </summary>
        public int MaxPoints { get; set; }
        /// <summary>
        /// текущее здоровье
        /// </summary>
        public int CurrentHealth { get; set; }
        /// <summary>
        /// текущая сила
        /// </summary>
        public int CurrentStrength { get; set; }
        /// <summary>
        /// текущая защита
        /// </summary>
        public int CurrentDefence { get; set; }
        /// <summary>
        /// текущий крит урон
        /// </summary>
        public decimal CurrentCriticalDamage { get; set; }
        /// <summary>
        /// текущий крит шанс
        /// </summary>
        public decimal CurrentCriticalChance { get; set; }
        /// <summary>
        /// максимальное здоровье, которого можно достичь на уровне
        /// </summary>
        public int MaxHealth { get; set; }
        /// <summary>
        /// максимальная сила, которого можно достичь на уровне
        /// </summary>
        public int MaxStrength { get; set; }
        /// <summary>
        /// максимальная защита, которого можно достичь на уровне
        /// </summary>

        public int MaxDefence { get; set; }
        /// <summary>
        /// максимальная крит урон, которого можно достичь на уровне
        /// </summary>

        public decimal MaxCriticalDamage { get; set; }
        /// <summary>
        /// максимальная крит шанс, которого можно достичь на уровне
        /// </summary>

        public decimal MaxCriticalChance { get; set; }
        /// <summary>
        /// средняя сила
        /// </summary>

        public decimal AveragePower { get; set; }
    }
}
