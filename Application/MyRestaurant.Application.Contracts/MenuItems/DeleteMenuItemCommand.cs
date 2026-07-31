using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.MenuItems
{
    public class DeleteMenuItemCommand : ICommand
    {
        public long Id { get; set; }
    }
}
