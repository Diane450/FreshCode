﻿using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class UserFood
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long FoodId { get; set; }

    public int Count { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
