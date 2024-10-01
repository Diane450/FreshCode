using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class ArtifactHistory
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long ArtifactId { get; set; }

    public DateTime GotAt { get; set; }

    public long BannerId { get; set; }

    public virtual Artifact Artifact { get; set; } = null!;

    public virtual Banner Banner { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
