using MyRestaurant.Framework.Querying.Pagination;

namespace MyRestaurant.Framework.HttpContext
{
    public interface IContextAccessor
    {
        void AddPaginationHeaders(PageMetadata pageMetadata);
    }
}
