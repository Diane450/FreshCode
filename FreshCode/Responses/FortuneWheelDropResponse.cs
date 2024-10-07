using FreshCode.Enums;
using FreshCode.ModelsDTO;

namespace FreshCode.Responses
{
    public class FortuneWheelDropResponse
    {
        public long BonusId { get; set; }
        public CharacteristicType Characteristic { get; set; }
        public int Value { get; set; }
        public BonusType BonusType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
