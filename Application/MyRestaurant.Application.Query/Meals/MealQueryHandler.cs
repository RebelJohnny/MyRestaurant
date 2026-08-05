using MyRestaurant.Application.Query.Contracts.Meals;
using MyRestaurant.EF.Read.Repositories.Meals;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Meals
{
    internal class MealQueryHandler(IMealQueryRepository repository) :
        IQueryHandler<GetMealFormDataQuery, MealFormData>,
        IQueryHandler<GetMealsQuery, IEnumerable<MealQueryResult>>
    {
        public async Task<MealFormData> Handle(GetMealFormDataQuery request, CancellationToken cancellationToken)
        {
            var meal = await repository.GetById(request.Id, cancellationToken);
            return meal;
        }

        public async Task<IEnumerable<MealQueryResult>> Handle(GetMealsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAll(cancellationToken);
        }
    }
}
