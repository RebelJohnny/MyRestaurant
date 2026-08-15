using MyRestaurant.Application.Query.Contracts.Meals;
using MyRestaurant.EF.Read.Repositories.Meals;
using MyRestaurant.Framework.Exceptions;
using MyRestaurant.Framework.HttpContext;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Meals
{
    internal class MealQueryHandler(IMealQueryRepository repository, IContextAccessor contextAccessor) :
        IQueryHandler<GetMealFormDataQuery, MealFormData>,
        IQueryHandler<GetMealListQuery, IEnumerable<MealQueryResult>>,
        IQueryHandler<GetAllMealsQuery, IEnumerable<MealQueryResult>>
    {
        public async Task<MealFormData> Handle(GetMealFormDataQuery request, CancellationToken cancellationToken)
        {
            var meal = await repository.GetById(request.Id, cancellationToken) ?? throw Error.NotFound;
            return meal;
        }

        public async Task<IEnumerable<MealQueryResult>> Handle(GetMealListQuery request, CancellationToken cancellationToken)
        {
            var list = await repository.GetList(request.QueryParams, cancellationToken);
            contextAccessor.AddPaginationHeaders(list.PageMetaData);
            return list.Items;
        }

        public async Task<IEnumerable<MealQueryResult>> Handle(GetAllMealsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAll(cancellationToken);
        }
    }
}
