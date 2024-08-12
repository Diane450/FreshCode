using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class ClanBattle
{
    public long Id { get; set; }

    public long FirstClanId { get; set; }

    public long SecondClanId { get; set; }

    public long WinnerId { get; set; }

    public int MoneyReward { get; set; }

    public int PointsReward { get; set; }

    public int StatPointsReward { get; set; }

    public virtual Clan FirstClan { get; set; } = null!;

    public virtual Clan SecondClan { get; set; } = null!;

    public virtual Clan Winner { get; set; } = null!;
}
