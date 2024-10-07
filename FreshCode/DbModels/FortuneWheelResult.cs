using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class FortuneWheelResult
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long BonusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public virtual Bonu Bonus { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
