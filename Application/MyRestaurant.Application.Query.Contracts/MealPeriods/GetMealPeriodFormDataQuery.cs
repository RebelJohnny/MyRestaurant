using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.MealPeriods
{
    public sealed class GetMealPeriodFormDataQuery : IQuery<MealPeriodQueryResult>
    {
        public long Id { get; set; }
    }
}
