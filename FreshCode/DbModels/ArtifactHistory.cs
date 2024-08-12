using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class ArtifactHistory
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public DateOnly GotAt { get; set; }

    public long ArtifactId { get; set; }

    public virtual Artifact Artifact { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
