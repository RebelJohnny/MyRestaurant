namespace MyRestaurant.Framework.Querying.Pagination
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;
        private int pageSize = 10;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        public int PageIndex { get; set; }
    }
}