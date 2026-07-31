using MyRestaurant.Application.Query.Contracts.Shared;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.MenuItems
{
    public class GetMenuItemsQuery : IQuery<IEnumerable<MenuItemQueryResult>>
    {
        public PaginationParams PaginationParams { get; set; }
    }
}
