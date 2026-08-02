using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.MealPeriods
{
    public sealed class ActivateMealPeriodCommand : ICommand
    {
        public long Id { get; set; }
    }
}
