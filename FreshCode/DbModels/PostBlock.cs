using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class PostBlock
{
    public long Id { get; set; }

    public long ContentTypeId { get; set; }

    public string Content { get; set; } = null!;

    public int Index { get; set; }

    public long PostId { get; set; }

    public virtual ContentType ContentType { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
