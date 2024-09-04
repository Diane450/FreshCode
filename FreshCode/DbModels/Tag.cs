using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Tag
{
    public long Id { get; set; }

    public string Tag1 { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
