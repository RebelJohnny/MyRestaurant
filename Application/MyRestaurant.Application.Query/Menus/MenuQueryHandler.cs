using MD.PersianDateTime;
using MyRestaurant.Application.Query.Contracts.Menus;
using MyRestaurant.EF.Read.Repositories.Meals;
using MyRestaurant.EF.Read.Repositories.Menus;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Menus
{
    internal class MenuQueryHandler(IMenuQueryRepository repository, IMealQueryRepository mealRepository) :
        IQueryHandler<GetMenuQuery, IEnumerable<MenuQueryResult>>,
        IQueryHandler<GetMealsForDayMealPeriodQuery, IEnumerable<MenuDayMeal>>
    {
        public async Task<IEnumerable<MenuQueryResult>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var targetDay = new PersianDateTime(DateTimeOffset.Now.AddDays(request.WeekDiff * 7).DateTime);
            var startDate = targetDay.GetFirstDayOfWeek().ToDateTime();
            var endDate = targetDay.GetPersianWeekend().ToDateTime();
            var targetedWeek = await repository.GetMenuBetweenDates(startDate, endDate, request.MealPeriodId, cancellationToken);
            var mealIds = targetedWeek.SelectMany(m => m.Meals).Select(ma => ma.Id);
            var meals = await mealRepository.GetByIds(mealIds, cancellationToken);
            var menuForWeek = MenuQueryMapper.Map(targetedWeek, meals);
            var datesWithMenu = menuForWeek.Select(x => x.Date);
            for (DateTime i = startDate; i.Date <= endDate.Date; i = i.AddDays(1))
            {
                if (!datesWithMenu.Any(x => x.Date == i.Date))
                {
                    menuForWeek.Add(new MenuQueryResult
                    {
                        Date = i,
                        DayOfWeek = (int)i.DayOfWeek,
                        Meals = []
                    });
                }
            }
            return menuForWeek.OrderBy(x => x.Date);
        }

        public async Task<IEnumerable<MenuDayMeal>> Handle(GetMealsForDayMealPeriodQuery request, CancellationToken cancellationToken)
        {
            var mealIds = await repository.GetMealIdsForDayMealPeriod(request.Date, request.MealPeriodId, cancellationToken);
            var meals = await mealRepository.GetByIds(mealIds, cancellationToken);
            return MenuQueryMapper.Map(meals);
        }
    }
}
