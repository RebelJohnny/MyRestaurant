namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public sealed class PersonnelQueryResult
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
