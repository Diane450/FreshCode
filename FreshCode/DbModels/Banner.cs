using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Banner
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public long BannerTypeId { get; set; }

    public virtual ICollection<ArtifactHistory> ArtifactHistories { get; set; } = new List<ArtifactHistory>();

    public virtual ICollection<BannerItem> BannerItems { get; set; } = new List<BannerItem>();

    public virtual BannerType BannerType { get; set; } = null!;
}
