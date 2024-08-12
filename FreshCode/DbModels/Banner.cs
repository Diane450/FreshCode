using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Banner
{
    public long Id { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly ExpiresAt { get; set; }

    public virtual ICollection<BannerItem> BannerItems { get; set; } = new List<BannerItem>();
}
