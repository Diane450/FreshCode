using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class BonusType
{
    public long Id { get; set; }

    public string Type { get; set; } = null!;
}
