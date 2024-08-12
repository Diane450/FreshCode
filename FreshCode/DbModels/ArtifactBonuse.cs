using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class ArtifactBonuse
{
    public long Id { get; set; }

    public long ArtifactId { get; set; }

    public long BonusId { get; set; }

    public virtual Artifact Artifact { get; set; } = null!;

    public virtual Bonu Bonus { get; set; } = null!;
}
