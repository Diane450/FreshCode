namespace FreshCode.Models
{
   /// <summary>
   /// Параметры пагинации
   /// </summary>
    public class QueryParameters
    {
        /// <summary>
        /// по какому полю сортировка
        /// </summary>
        public string SortBy { get; set; } = "Id"; // Поле для сортировки по умолчанию
        /// <summary>
        /// по какому полю фильтрация
        /// </summary>
        public string? FilterBy { get; set; }
        /// <summary>
        /// Значение для фильтрации
        /// </summary>
        public string? FilterValue { get; set; }
        /// <summary>
        /// Сортировка по убыванию (true/false)
        /// </summary>
        public bool SortDescending { get; set; } = false;
        /// <summary>
        /// какая по счету страница. Дефолтное значение = 1
        /// </summary>
        public int Page { get; set; } = 1;
        /// <summary>
        /// кол-во объектов на странице. Дефолтное значение = 5
        /// </summary>
        public int PageSize { get; set; } = 5;
    }
}
