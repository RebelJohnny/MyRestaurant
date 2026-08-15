using MyRestaurant.Framework.Mediator;
using MyRestaurant.Framework.Querying;

namespace MyRestaurant.Application.Query.Contracts.MealPeriods
{
    public sealed class GetMealPeriodListQuery : IQuery<IEnumerable<MealPeriodQueryResult>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
