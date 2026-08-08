using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.Menus
{
    public sealed class GetMealsForDayMealPeriodQuery: IQuery<IEnumerable<MenuDayMeal>>
    {
        public DateTimeOffset Date { get; set; }
        public long MealPeriodId { get; set; }
    }
}
