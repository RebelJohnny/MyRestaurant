using MyRestaurant.Framework.Querying.Filters;
using MyRestaurant.Framework.Querying.Pagination;
using MyRestaurant.Framework.Querying.Sorts;

namespace MyRestaurant.Framework.Querying
{
    public class QueryParams
    {
        public PaginationParams PaginationParams { get; set; }
        public List<FilterParams> Filters { get; set; }
        public List<SortParams> Sorts { get; set; }
    }
}
