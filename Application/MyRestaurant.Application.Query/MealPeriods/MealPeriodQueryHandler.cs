using MyRestaurant.Application.Query.Contracts.MealPeriods;
using MyRestaurant.EF.Read.Repositories.MealPeriods;
using MyRestaurant.Framework.Exceptions;
using MyRestaurant.Framework.HttpContext;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.MealPeriods
{
    internal class MealPeriodQueryHandler(IMealPeriodQueryRepository repository, IContextAccessor contextAccessor) :
        IQueryHandler<GetMealPeriodFormDataQuery, MealPeriodQueryResult>,
        IQueryHandler<GetMealPeriodListQuery, IEnumerable<MealPeriodQueryResult>>,
        IQueryHandler<GetAllMealPeriodsQuery, IEnumerable<MealPeriodQueryResult>>
    {
        public async Task<MealPeriodQueryResult> Handle(GetMealPeriodFormDataQuery request, CancellationToken cancellationToken)
        {
            var mealPeriod = await repository.GetById(request.Id, cancellationToken) ?? throw Error.NotFound;
            return mealPeriod;
        }

        public async Task<IEnumerable<MealPeriodQueryResult>> Handle(GetMealPeriodListQuery request, CancellationToken cancellationToken)
        {
            var list = await repository.GetList(request.QueryParams, cancellationToken);
            contextAccessor.AddPaginationHeaders(list.PageMetaData);
            return list.Items;
        }

        public async Task<IEnumerable<MealPeriodQueryResult>> Handle(GetAllMealPeriodsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAll(cancellationToken);
        }
    }
}
