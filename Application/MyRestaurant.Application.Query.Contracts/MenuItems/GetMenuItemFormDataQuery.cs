using MediatR;

namespace MyRestaurant.Application.Query.Contracts.MenuItems
{
    public class GetMenuItemFormDataQuery : IRequest<MenuItemFormData>
    {
        public long Id { get; set; }
    }
}
