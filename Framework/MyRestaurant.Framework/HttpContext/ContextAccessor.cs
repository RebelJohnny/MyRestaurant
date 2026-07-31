using Microsoft.AspNetCore.Http;

namespace MyRestaurant.Framework.HttpContext
{
    public class ContextAccessor(IHttpContextAccessor httpContextAccessor) : IContextAccessor
    {
        // FUTURE PHASES: adding headers for pagination, location, etc
    }
}
