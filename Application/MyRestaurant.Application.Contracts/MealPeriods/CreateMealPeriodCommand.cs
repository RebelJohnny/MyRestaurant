using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.MealPeriods
{
    public class CreateMealPeriodCommand : ICommand<MealPeriodDTO>
    {
        public string Name { get; set; }
        public int Time { get; set; }
    }
}
