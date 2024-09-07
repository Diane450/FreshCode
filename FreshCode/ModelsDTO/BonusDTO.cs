namespace FreshCode.ModelsDTO
{
    public class BonusDTO
    {
        public long BonusId { get; set; }
        public string Characteristic { get; set; } = null!;
        public int Value { get; set; }
        public BonusType BonusType { get; set; }
    }
    public enum BonusType
    {
        flat,
        percent
    }
}
