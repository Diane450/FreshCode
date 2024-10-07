using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class BonusFormat
{
    public long Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Bonu> Bonus { get; set; } = new List<Bonu>();

    public virtual ICollection<Characteristic> Characteristics { get; set; } = new List<Characteristic>();
}
