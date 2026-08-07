using MD.PersianDateTime;
using MyRestaurant.Application.Query.Contracts.Menus;
using MyRestaurant.EF.Read.Repositories.Meals;
using MyRestaurant.EF.Read.Repositories.Menus;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Menus
{
    internal class MenuQueryHandler(IMenuQueryRepository repository, IMealQueryRepository mealRepository) :
        IQueryHandler<GetMenuQuery, IEnumerable<MenuQueryResult>>
    {
        public async Task<IEnumerable<MenuQueryResult>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var targetDay = new PersianDateTime(DateTimeOffset.Now.AddDays(request.WeekDiff * 7).DateTime);
            var startDate = targetDay.GetFirstDayOfWeek().ToDateTime();
            var endDate = targetDay.GetPersianWeekend().ToDateTime();
            var targetedWeek = await repository.GetMenuBetweenDates(startDate, endDate, request.MealPeriodId, cancellationToken);
            var mealIds = targetedWeek.SelectMany(m => m.Articles).Select(ma => ma.MealId);
            var meals = await mealRepository.GetByIds(mealIds, cancellationToken);
            var menuForWeek = MenuQueryMapper.Map(targetedWeek, meals);
            var datesWithMenu = menuForWeek.Select(x => x.Date.Date);
            for (DateTime i = startDate; i.Date <= endDate.Date; i.AddDays(1))
            {
                if (!datesWithMenu.Any(x => x == i))
                {
                    menuForWeek.Add(new MenuQueryResult
                    {
                        Date = i,
                        DayOfWeek = i.DayOfWeek,
                        Meals = []
                    });
                }
            }
            return menuForWeek.OrderBy(x => x.Date);
        }
    }
}
