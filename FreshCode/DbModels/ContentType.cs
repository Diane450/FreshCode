using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class ContentType
{
    public long Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<PostBlock> PostBlocks { get; set; } = new List<PostBlock>();
}
