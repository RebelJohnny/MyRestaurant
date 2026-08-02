using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.MenuItems
{
    public sealed class DeleteMenuItemCommand : ICommand
    {
        public long Id { get; set; }
    }
}
