namespace MyRestaurant.Domain.Personnels.Args
{
    public sealed record PersonnelReserveArgs
    {
        public required DateTimeOffset Date { get; init; }
        public required List<PersonnelReservedMealArgs> Meals { get; init; }
    }
    public sealed record PersonnelReservedMealArgs
    {
        public required long Id { get; init; }
        public required long MealPeriodId { get; init; }
        public required short Count { get; init; }
    }
}
