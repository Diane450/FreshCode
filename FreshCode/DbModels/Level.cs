using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Level
{
    public long Id { get; set; }

    public int MaxPoints { get; set; }

    public int MaxHealth { get; set; }

    public int MaxStrength { get; set; }

    public int MaxDefence { get; set; }

    public int MaxCriticalDamage { get; set; }

    public int MaxCriticalChance { get; set; }

    public decimal EnhancementCoefficient { get; set; }
}
