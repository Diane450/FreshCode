using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class FoodBonuse
{
    public long Id { get; set; }

    public long FoodId { get; set; }

    public long BonusId { get; set; }

    public bool IsTemporary { get; set; }

    public virtual Bonu Bonus { get; set; } = null!;

    public virtual Food Food { get; set; } = null!;
}
