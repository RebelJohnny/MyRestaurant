using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.MenuItems
{
    public sealed class GetMenuItemFormDataQuery : IQuery<MenuItemFormData>
    {
        public long Id { get; set; }
    }
}
