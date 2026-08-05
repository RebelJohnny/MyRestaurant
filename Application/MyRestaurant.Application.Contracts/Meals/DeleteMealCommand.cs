using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Meals
{
    public sealed class DeleteMealCommand : ICommand
    {
        public long Id { get; set; }
    }
}
