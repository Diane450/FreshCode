using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class UserTask
{
    public long Id { get; set; }

    public long TaskId { get; set; }

    public long UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public bool IsRewardReceived { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
