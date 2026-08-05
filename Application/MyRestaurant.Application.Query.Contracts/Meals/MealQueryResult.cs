namespace MyRestaurant.Application.Query.Contracts.Meals
{
    public sealed class MealQueryResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public short Type { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
