using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class PostRating
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public bool Rating { get; set; }

    public long? PostId { get; set; }

    public virtual Post? Post { get; set; }

    public virtual User User { get; set; } = null!;
}
