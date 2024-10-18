using FreshCode.DbModels;

namespace FreshCode.ModelsDTO
{
    /// <summary>
    /// DTO артефакты
    /// </summary>
    public class ArtifactDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Значение Х в тайловой карте
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Значение У в тайловой карте
        /// </summary>

        public int Y { get; set; }
        /// <summary>
        /// Цена в магазине
        /// </summary>

        public int? Price { get; set; }
        /// <summary>
        /// редкость
        /// </summary>

        public string? Rarity { get; set; } = null!;
        /// <summary>
        /// бонусы артефакта
        /// </summary>

        public List<BonusDTO>? Bonuses { get; set; } = null!;
        /// <summary>
        /// Тип: шапка или аксессуар
        /// </summary>

        public string? Type { get; set; } = null!;
    }
}
