using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.Meals
{
    public sealed class GetMealFormDataQuery : IQuery<MealFormData>
    {
        public long Id { get; set; }
    }
}
