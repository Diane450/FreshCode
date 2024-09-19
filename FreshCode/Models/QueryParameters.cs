namespace FreshCode.Models
{
    public class QueryParameters
    {
        public string SortBy { get; set; } = "Id"; // Поле для сортировки по умолчанию
        public string? FilterBy { get; set; }
        public string? FilterValue { get; set; }
        public bool SortDescending { get; set; } = false;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
