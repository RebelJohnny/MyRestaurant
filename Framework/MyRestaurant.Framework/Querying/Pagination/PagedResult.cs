namespace MyRestaurant.Framework.Querying.Pagination
{
    public sealed record PagedResult<T>
    {
        public List<T> Items{ get; private set; }
        public PageMetadata PageMetaData { get; private set; }
        public PagedResult(int pageIndex, int PageSize, int totalCount, List<T> items)
        {
            Items = items;
            PageMetaData = new PageMetadata(pageIndex, PageSize, totalCount);
        }
    }
    public sealed record PageMetadata
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPrevious => PageIndex > 0;
        public bool HasNext => PageIndex + 1 < TotalPages;
        public PageMetadata(int pageIndex, int pageSize, int totalCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
