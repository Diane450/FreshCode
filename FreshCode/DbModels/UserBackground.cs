using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class UserBackground
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long BackgroundId { get; set; }

    public virtual Background Background { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
