using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.MealPeriods
{
    public class DeleteMealPeriodCommand : ICommand
    {
        public long Id { get; set; }
    }
}
