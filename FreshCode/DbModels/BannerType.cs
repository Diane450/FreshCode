using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class BannerType
{
    public long Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Banner> Banners { get; set; } = new List<Banner>();
}
