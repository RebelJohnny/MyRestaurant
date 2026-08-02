namespace MyRestaurant.Domain.Personnels.Args
{
    public sealed record PersonnelArgs
    {
        public required string Code { get; init; }
        public required string Name { get; init; }
    }
}
