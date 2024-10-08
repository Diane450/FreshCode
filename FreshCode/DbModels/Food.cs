using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Food
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public virtual ICollection<FoodBonuse> FoodBonuses { get; set; } = new List<FoodBonuse>();

    public virtual ICollection<PetFeedLog> PetFeedLogs { get; set; } = new List<PetFeedLog>();

    public virtual ICollection<UserFood> UserFoods { get; set; } = new List<UserFood>();
}
