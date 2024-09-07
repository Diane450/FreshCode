using FreshCode.DbModels;

namespace FreshCode.ModelsDTO
{
    public class ArtifactDTO
    {
        public long ArtifactId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Price { get; set; }

        public string Rarity { get; set; } = null!;

        public List<BonusDTO> Bonuses { get; set; } = null!;

        public string Type { get; set; } = null!;
    }
}
