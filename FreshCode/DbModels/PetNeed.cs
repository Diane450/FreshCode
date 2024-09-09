using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class PetNeed
{
    public long Id { get; set; }

    public string Need { get; set; } = null!;

    public virtual ICollection<FoodNeed> FoodNeeds { get; set; } = new List<FoodNeed>();
}
