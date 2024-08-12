using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Task
{
    public long Id { get; set; }

    public string Descryption { get; set; } = null!;

    public int MoneyReward { get; set; }

    public int PointsReward { get; set; }

    public int StatPointsReward { get; set; }

    public int PrimogemsReward { get; set; }

    public virtual ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
}
