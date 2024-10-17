using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class BattleQueue
{
    public long Id { get; set; }

    public long PetId { get; set; }

    public long UserId { get; set; }

    public int PetLevel { get; set; }

    public DateTime CreatedAt { get; set; }

    public long? BattleId { get; set; }

    public virtual UserBattle? Battle { get; set; }

    public virtual Pet Pet { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
