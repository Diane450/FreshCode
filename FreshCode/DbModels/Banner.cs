using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Banner
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public virtual ICollection<BannerItem> BannerItems { get; set; } = new List<BannerItem>();
}
