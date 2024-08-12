using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class ArtifactType
{
    public long Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Artifact> Artifacts { get; set; } = new List<Artifact>();
}
