namespace VaccinationCard.Domain.Shared
{
    public class PaginatedResponse<T>(IEnumerable<T> items, int totalItems, int pageNumber, int pageSize)
    {
        public IEnumerable<T> Items { get; } = items;
        public int PageNumber { get; } = pageNumber;
        public int PageSize { get; } = pageSize;
        public int TotalItems { get; } = totalItems;
        public int TotalPages { get; } = (int)Math.Ceiling(totalItems / (double)pageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
