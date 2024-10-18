using FreshCode.Enums;
using FreshCode.ModelsDTO;

namespace FreshCode.Responses
{
    /// <summary>
    /// Дроп с колеса фортуны
    /// </summary>
    public class FortuneWheelDropResponse
    {
        /// <summary>
        /// Характеристика
        /// </summary>
        public string Characteristic { get; set; } = null!;
        /// <summary>
        /// Значение
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Формат (число или процент)
        /// </summary>
        public string BonusFormat { get; set; } = null!;
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Дата просрочки
        /// </summary>
        public DateTime? ExpiresAt { get; set; }
    }
}
