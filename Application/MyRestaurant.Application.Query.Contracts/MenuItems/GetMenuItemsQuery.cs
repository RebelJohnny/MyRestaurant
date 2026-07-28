using MediatR;

namespace MyRestaurant.Application.Query.Contracts.MenuItems
{
    public class GetMenuItemsQuery : IRequest<IEnumerable<MenuItemQueryResult>>
    {
    }
}
