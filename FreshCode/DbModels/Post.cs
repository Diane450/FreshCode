using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Post
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Title { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public long TagId { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
