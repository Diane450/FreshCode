using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class ContentType
{
    public string Type { get; set; } = null!;

    public long Id { get; set; }

    public virtual ICollection<PostBlock> PostBlocks { get; set; } = new List<PostBlock>();
}
