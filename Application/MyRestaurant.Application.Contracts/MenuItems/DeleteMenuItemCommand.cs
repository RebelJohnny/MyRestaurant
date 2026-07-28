using MediatR;

namespace MyRestaurant.Application.Contracts.MenuItems
{
    public class DeleteMenuItemCommand : IRequest
    {
        public long Id { get; set; }
    }
}
