using MyRestaurant.Application.Contracts.Menus;
using MyRestaurant.Domain.Menus.Args;

namespace MyRestaurant.Application.Menus
{
    internal static class MenuMapper
    {
        internal static MenuArgs Map(UpdateMenuOnDayCommand commmand)
        {
            return new MenuArgs
            {
                Date = commmand.Date,
                MealPeriodId = commmand.MealPeriodId,
                MealIds = commmand.MealIds
            };
        }
    }
}
