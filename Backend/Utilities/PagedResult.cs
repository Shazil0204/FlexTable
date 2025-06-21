namespace Backend.utilities
{
    public class PagedResult<T>
    {
        public required List<T> Results { get; set; }
        public int TotalCount { get; set; }
    }
}