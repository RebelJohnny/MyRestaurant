using MediatR;
using MyRestaurant.Domain.MenuItems.Enums;

namespace MyRestaurant.Application.Contracts.MenuItems
{
    public class UpdateMenuItemCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public MenuItemTypeEnum Type { get; set; }
    }
}
