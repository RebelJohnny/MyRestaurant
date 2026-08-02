using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.MealPeriods
{
    public sealed class DeactivateMealPeriodCommand : ICommand
    {
        public long Id { get; set; }
    }
}
