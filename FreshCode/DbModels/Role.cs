using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Role
{
    public long Id { get; set; }

    public string Role1 { get; set; } = null!;

    public virtual ICollection<UserClan> UserClans { get; set; } = new List<UserClan>();
}
