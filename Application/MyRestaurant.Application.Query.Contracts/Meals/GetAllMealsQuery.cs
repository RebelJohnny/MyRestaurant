using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.Meals
{
    public sealed class GetAllMealsQuery : IQuery<IEnumerable<MealQueryResult>>
    {
    }
}
