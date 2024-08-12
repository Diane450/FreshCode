namespace FreshCode.ModelsDTO
{
    public class ArtifactDTO
    {
        public long Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Price { get; set; }

        public string Rarity { get; set; } = null!;

        public string Bonus { get; set; } = null!;

        public string Characteristic { get; set; } = null!;

        public decimal Value { get; set; }

        public string Type { get; set; } = null!;
    }
}
