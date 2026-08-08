using MD.PersianDateTime;
using MyRestaurant.Application.Query.Contracts.Personnels;
using MyRestaurant.EF.Read.Repositories.Meals;
using MyRestaurant.EF.Read.Repositories.Personnels;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Personnel
{
    internal class PersonnelQueryHandler(IPersonnelQueryRepository repository, IMealQueryRepository mealRepository) :
        IQueryHandler<GetPersonnelFormDataQuery, PersonnelFormData>,
        IQueryHandler<GetPersonnelQuery, IEnumerable<PersonnelQueryResult>>,
        IQueryHandler<GetPersonnelReservedOrdersQuery, IEnumerable<PersonnelReserveQueryResult>>
    {
        public async Task<PersonnelFormData> Handle(GetPersonnelFormDataQuery request, CancellationToken cancellationToken)
        {
            var personnel = await repository.GetById(request.Id, cancellationToken);
            return personnel;
        }

        public async Task<IEnumerable<PersonnelQueryResult>> Handle(GetPersonnelQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAll(cancellationToken);
        }

        public async Task<IEnumerable<PersonnelReserveQueryResult>> Handle(GetPersonnelReservedOrdersQuery request, CancellationToken cancellationToken)
        {
            var targetDay = new PersianDateTime(DateTimeOffset.Now.AddDays(request.WeekDiff * 7).DateTime);
            var startDate = targetDay.GetFirstDayOfWeek().ToDateTime();
            var endDate = targetDay.GetPersianWeekend().ToDateTime();
            var targetedWeek = await repository.GetReservedOrdersBetweenDates(request.PersonnelId, startDate, endDate, request.MealPeriodId, cancellationToken);
            var mealIds = targetedWeek.SelectMany(m => m.Articles).Select(ma => ma.Id);
            var meals = await mealRepository.GetByIds(mealIds, cancellationToken);
            var reservedOrdersForWeek = PersonnelQueryMapper.Map(targetedWeek, meals);
            var datesWithReservedOrders = reservedOrdersForWeek.Select(x => x.Date);
            for (DateTime i = startDate; i.Date <= endDate.Date; i = i.AddDays(1))
            {
                if (!datesWithReservedOrders.Any(x => x.Date == i.Date))
                {
                    reservedOrdersForWeek.Add(new PersonnelReserveQueryResult
                    {
                        Date = i,
                        DayOfWeek = i.DayOfWeek,
                        Meals = []
                    });
                }
            }
            return reservedOrdersForWeek.OrderBy(x => x.Date);
        }
    }
}
