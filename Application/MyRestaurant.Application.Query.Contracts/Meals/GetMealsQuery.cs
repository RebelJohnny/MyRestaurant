using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.Meals
{
    public sealed class GetMealsQuery : IQuery<IEnumerable<MealQueryResult>>
    {
        //public PaginationParams PaginationParams { get; set; }
    }
}
