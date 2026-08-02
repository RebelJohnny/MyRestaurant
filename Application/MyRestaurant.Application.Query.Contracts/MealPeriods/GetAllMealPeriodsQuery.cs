using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.MealPeriods
{
    public sealed class GetAllMealPeriodsQuery : IQuery<IEnumerable<MealPeriodQueryResult>>
    {
    }
}
