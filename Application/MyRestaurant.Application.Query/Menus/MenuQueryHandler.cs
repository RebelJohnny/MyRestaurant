using MD.PersianDateTime;
using MyRestaurant.Application.Query.Contracts.Menus;
using MyRestaurant.EF.Read.Repositories.Menus;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Menus
{
    internal class MenuQueryHandler(IMenuQueryRepository repository) :
        IQueryHandler<GetMenuQuery, IEnumerable<MenuQueryResult>>
    {
        public Task<IEnumerable<MenuQueryResult>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var targetDay = new PersianDateTime(DateTimeOffset.Now.AddDays(request.WeekDiff * 7).DateTime);
            var startDate = targetDay.GetFirstDayOfWeek().ToDateTime();
            var endDate = targetDay.GetPersianWeekend().ToDateTime();
            throw new NotImplementedException();
        }
    }
}
