using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class UserBattle
{
    public long Id { get; set; }

    public long FirstPlayerId { get; set; }

    public long SecondPlayerId { get; set; }

    public long? WinnerId { get; set; }

    public int? MoneyReward { get; set; }

    public int? PointsReward { get; set; }

    public int? StatPointsReward { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public int? PrimogemsReward { get; set; }

    public virtual User FirstPlayer { get; set; } = null!;

    public virtual User SecondPlayer { get; set; } = null!;

    public virtual User? Winner { get; set; }
}
