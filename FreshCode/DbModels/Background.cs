using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Background
{
    public long Id { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public int Price { get; set; }

    public virtual ICollection<UserBackground> UserBackgrounds { get; set; } = new List<UserBackground>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
