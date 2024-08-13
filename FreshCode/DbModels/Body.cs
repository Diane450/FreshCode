using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Body
{
    public long Id { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
