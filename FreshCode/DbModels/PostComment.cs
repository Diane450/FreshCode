using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class PostComment
{
    public long UserId { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long PostId { get; set; }

    public long Id { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
