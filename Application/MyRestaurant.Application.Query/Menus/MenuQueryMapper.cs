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
                DayOfWeek = (int)m.Date.DayOfWeek,
                Meals = m.Meals.Select(ma =>
                {
                    var meal = meals.First(x => x.Id == ma.Id);
                    return new MenuDayMeal
                    {
                        Id = ma.Id,
                        Name = meal.Name,
                        Type = meal.Type
                    };
                })
            })];
        }
        internal static List<MenuDayMeal> Map(List<MealFormData> meals)
        {
            return [..meals.Select(m => new MenuDayMeal
            {
                Id= m.Id,
                Name= m.Name,
                Type = m.Type
            })];
        }
    }
}
