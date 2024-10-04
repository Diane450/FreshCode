using FreshCode.Enums;
using FreshCode.ModelsDTO;

namespace FreshCode.Responses
{
    public class FortuneWheelDropResponse
    {
        public CharacteristicType Characteristic { get; set; }
        public int Value { get; set; }
        public BonusType BonusType { get; set; }
    }
}
