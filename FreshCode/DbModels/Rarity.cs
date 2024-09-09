using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Rarity
{
    public long Id { get; set; }

    public string Rarity1 { get; set; } = null!;

    public virtual ICollection<Artifact> Artifacts { get; set; } = new List<Artifact>();
}
