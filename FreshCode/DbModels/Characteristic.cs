using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Characteristic
{
    public long Id { get; set; }

    public string Characteristic1 { get; set; } = null!;

    public long? CharacteristicType { get; set; }

    public virtual ICollection<Bonu> Bonus { get; set; } = new List<Bonu>();

    public virtual BonusFormat? CharacteristicTypeNavigation { get; set; }
}
