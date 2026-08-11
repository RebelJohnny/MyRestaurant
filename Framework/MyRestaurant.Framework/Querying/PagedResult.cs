namespace MyRestaurant.Framework.Querying
{
    public sealed record PagedResult<T>(
    IReadOnlyList<T> Items,
    int PageIndex,
    int PageSize,
    int TotalCount)
    {
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        public bool HasNext => PageIndex + 1 < TotalPages;

        public bool HasPrevious => PageIndex > 0;
    }
}
