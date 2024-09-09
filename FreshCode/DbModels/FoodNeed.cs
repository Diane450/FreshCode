using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class FoodNeed
{
    public long Id { get; set; }

    public long FoodId { get; set; }

    public long NeedId { get; set; }

    public int Amount { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual PetNeed Need { get; set; } = null!;
}
