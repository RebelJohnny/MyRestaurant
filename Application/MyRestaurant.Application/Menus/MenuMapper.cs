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
                Articles = [.. commmand.Articles.Select(a => new MenuArticleArgs 
                { 
                    Id = a.Id,
                    MealId = a.MealId,
                    MealPeriodId = a.MealPeriodId,
                })]
            };
        }
    }
}
