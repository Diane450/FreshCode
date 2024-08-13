namespace FreshCode.ModelsDTO
{
    public class BonusDTO
    {
        public long Id { get; set; }
        public string Characteristic { get; set; } = null!;
        public int Value { get; set; }
        public string Type { get; set; } = null!;
    }
}
