using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class UserFortuneWheelSpin
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
