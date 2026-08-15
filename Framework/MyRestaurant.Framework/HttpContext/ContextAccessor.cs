using Microsoft.AspNetCore.Http;
using MyRestaurant.Framework.Querying.Pagination;

namespace MyRestaurant.Framework.HttpContext
{
    public class ContextAccessor(IHttpContextAccessor httpContextAccessor) : IContextAccessor
    {
        public void AddPaginationHeaders(PageMetadata pageMetadata)
        {
            httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", pageMetadata.TotalCount.ToString());
            httpContextAccessor.HttpContext.Response.Headers.Add("X-Page-Index", pageMetadata.PageIndex.ToString());
            httpContextAccessor.HttpContext.Response.Headers.Add("X-Page-Size", pageMetadata.PageSize.ToString());
            httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Pages", pageMetadata.PageSize.ToString());
        }
    }
}
