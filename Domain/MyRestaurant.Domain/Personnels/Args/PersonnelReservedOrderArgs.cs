namespace MyRestaurant.Domain.Personnels.Args
{
    public sealed record PersonnelReservedOrderArgs
    {
        public required DateTimeOffset Date { get; init; }
        public required List<PersonnelReservedOrderArticleArgs> Articles { get; init; }
    }
    public sealed record PersonnelReservedOrderArticleArgs
    {
        public required long? Id { get; init; }
        public required long MealPeriodId { get; init; }
        public required long MealId { get; init; }
        public required short Count { get; init; }
    }
}
