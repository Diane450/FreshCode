using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Pet
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long UserId { get; set; }

    public long BodyId { get; set; }

    public long EyesId { get; set; }

    public long? HatId { get; set; }

    public long? AccessoryId { get; set; }

    public int SleepNeed { get; set; }

    public int FeedNeed { get; set; }

    public int FightNeed { get; set; }

    public decimal GeneralHappiness { get; set; }

    public long LevelId { get; set; }

    public int Points { get; set; }

    public int CurrentHealth { get; set; }

    public int CurrentStrength { get; set; }

    public int CurrentDefence { get; set; }

    public decimal CurrentCriticalDamage { get; set; }

    public decimal CurrentCriticalChance { get; set; }

    public decimal AveragePower { get; set; }

    public int MaxPoints { get; set; }

    public virtual Artifact? Accessory { get; set; }

    public virtual Body Body { get; set; } = null!;

    public virtual Eye Eyes { get; set; } = null!;

    public virtual Artifact? Hat { get; set; }

    public virtual Level Level { get; set; } = null!;

    public virtual ICollection<PetSleepLog> PetSleepLogs { get; set; } = new List<PetSleepLog>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserBonuse> UserBonuses { get; set; } = new List<UserBonuse>();
}
