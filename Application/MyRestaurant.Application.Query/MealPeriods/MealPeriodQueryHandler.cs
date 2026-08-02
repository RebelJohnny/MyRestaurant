using MyRestaurant.Application.Query.Contracts.MealPeriods;
using MyRestaurant.EF.Read.Repositories.MealPeriods;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.MealPeriods
{
    internal class MealPeriodQueryHandler(IMealPeriodQueryRepository repository) :
        IQueryHandler<GetMealPeriodFormDataQuery, MealPeriodQueryResult>,
        IQueryHandler<GetAllMealPeriodsQuery, IEnumerable<MealPeriodQueryResult>>
    {
        public async Task<MealPeriodQueryResult> Handle(GetMealPeriodFormDataQuery request, CancellationToken cancellationToken)
        {
            var mealPeriod = await repository.GetById(request.Id, cancellationToken);
            return mealPeriod;
        }

        public async Task<IEnumerable<MealPeriodQueryResult>> Handle(GetAllMealPeriodsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAll(cancellationToken);
        }
    }
}
