﻿using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class User
{
    public long Id { get; set; }

    public int Money { get; set; }

    public int StatPoints { get; set; }

    public long BackgroundId { get; set; }

    public int WonBattlesCount { get; set; }

    public long? VkId { get; set; }

    public virtual ICollection<ArtifactHistory> ArtifactHistories { get; set; } = new List<ArtifactHistory>();

    public virtual Background Background { get; set; } = null!;

    public virtual ICollection<FortuneWheelResult> FortuneWheelResults { get; set; } = new List<FortuneWheelResult>();

    public virtual ICollection<UserArtifact> UserArtifacts { get; set; } = new List<UserArtifact>();

    public virtual ICollection<UserBackground> UserBackgrounds { get; set; } = new List<UserBackground>();

    public virtual ICollection<UserBattle> UserBattleFirstPlayers { get; set; } = new List<UserBattle>();

    public virtual ICollection<UserBattle> UserBattleSecondPlayers { get; set; } = new List<UserBattle>();

    public virtual ICollection<UserBattle> UserBattleWinners { get; set; } = new List<UserBattle>();

    public virtual ICollection<UserClan> UserClans { get; set; } = new List<UserClan>();

    public virtual ICollection<UserFood> UserFoods { get; set; } = new List<UserFood>();

    public virtual ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
}
