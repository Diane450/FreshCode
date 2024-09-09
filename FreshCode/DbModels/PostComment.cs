﻿using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class PostComment
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Comment { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }

    public DateOnly UpdatedAt { get; set; }

    public long PostId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
