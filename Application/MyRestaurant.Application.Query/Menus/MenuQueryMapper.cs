using MyRestaurant.Application.Query.Contracts.Meals;
using MyRestaurant.Application.Query.Contracts.Menus;

namespace MyRestaurant.Application.Query.Menus
{
    internal static class MenuQueryMapper
    {
        internal static List<MenuQueryResult> Map(List<MenuOnMealPeriodQueryResult> menus, List<MealFormData> meals)
        {
            return [.. menus.Select(m => new MenuQueryResult
            {
                Date = m.Date,
                DayOfWeek = m.Date.DayOfWeek,
                Meals = m.Articles.Select(ma =>
                {
                    var meal = meals.First(x => x.Id == ma.MealId);
                    return new MenuDayMeal
                    {
                        Id = ma.Id,
                        Name = meal.Name,
                        Type = meal.Type
                    };
                })
            })];
        }
    }
}
