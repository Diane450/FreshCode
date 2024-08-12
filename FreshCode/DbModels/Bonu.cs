using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Bonu
{
    public long Id { get; set; }

    public long CharacteristicId { get; set; }

    public int Value { get; set; }

    public long TypeId { get; set; }

    public virtual ICollection<ArtifactBonuse> ArtifactBonuses { get; set; } = new List<ArtifactBonuse>();

    public virtual Characteristic Characteristic { get; set; } = null!;

    public virtual ICollection<FoodBonuse> FoodBonuses { get; set; } = new List<FoodBonuse>();

    public virtual ICollection<FortuneWheelResult> FortuneWheelResults { get; set; } = new List<FortuneWheelResult>();

    public virtual BonusType Type { get; set; } = null!;
}
