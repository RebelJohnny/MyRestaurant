using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.MealPeriods
{
    public sealed class UpdateMealPeriodCommand : ICommand 
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Time { get; set; }
    }
}
