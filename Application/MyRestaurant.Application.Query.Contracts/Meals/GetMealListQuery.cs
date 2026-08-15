using MyRestaurant.Framework.Mediator;
using MyRestaurant.Framework.Querying;

namespace MyRestaurant.Application.Query.Contracts.Meals
{
    public sealed class GetMealListQuery : IQuery<IEnumerable<MealQueryResult>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
