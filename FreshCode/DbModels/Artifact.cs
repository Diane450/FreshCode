using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Artifact
{
    public long Id { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public long RarityId { get; set; }

    public long ArtifactTypeId { get; set; }

    public int Price { get; set; }

    public virtual ICollection<ArtifactBonuse> ArtifactBonuses { get; set; } = new List<ArtifactBonuse>();

    public virtual ICollection<ArtifactHistory> ArtifactHistories { get; set; } = new List<ArtifactHistory>();

    public virtual ArtifactType ArtifactType { get; set; } = null!;

    public virtual ICollection<BannerItem> BannerItems { get; set; } = new List<BannerItem>();

    public virtual ICollection<Pet> PetAccessories { get; set; } = new List<Pet>();

    public virtual ICollection<Pet> PetHats { get; set; } = new List<Pet>();

    public virtual Rarity Rarity { get; set; } = null!;

    public virtual ICollection<UserArtifact> UserArtifacts { get; set; } = new List<UserArtifact>();
}
