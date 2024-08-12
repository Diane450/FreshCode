using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class BannerItem
{
    public long Id { get; set; }

    public long BannerId { get; set; }

    public long ArtifactId { get; set; }

    public virtual Artifact Artifact { get; set; } = null!;

    public virtual Banner Banner { get; set; } = null!;
}
