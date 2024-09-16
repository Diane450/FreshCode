namespace FreshCode.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        public PagedResult(List<T> items, int page, int pageSize)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
        }
    }

}
