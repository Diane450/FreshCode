using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class UserClan
{
    public long Id { get; set; }

    public long ClanId { get; set; }

    public long UserId { get; set; }

    public long RoleId { get; set; }

    public virtual Clan Clan { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
