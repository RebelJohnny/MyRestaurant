using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.MenuItems
{
    public sealed class GetMenuItemsQuery : IQuery<IEnumerable<MenuItemQueryResult>>
    {
        //public PaginationParams PaginationParams { get; set; }
    }
}
