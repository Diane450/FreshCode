namespace FreshCode.ModelsDTO
{
    public class BonusDTO
    {
        public long Id { get; set; }
        public string Characteristic { get; set; } = null!;
        public int Value { get; set; }
        public BonusType Type { get; set; }
    }
    public enum BonusType
    {
        Flat,
        Percentage
    }
}
