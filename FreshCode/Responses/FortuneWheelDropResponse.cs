using FreshCode.Enums;
using FreshCode.ModelsDTO;

namespace FreshCode.Responses
{
    public class FortuneWheelDropResponse
    {
        public string Characteristic { get; set; } = null!;
        public int Value { get; set; }
        public string BonusFormat { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
