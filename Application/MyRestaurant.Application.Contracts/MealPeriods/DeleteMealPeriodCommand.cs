using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.MealPeriods
{
    public sealed class DeleteMealPeriodCommand : ICommand
    {
        public long Id { get; set; }
    }
}
