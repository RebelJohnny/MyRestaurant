namespace MyRestaurant.Application.Query.Contracts.MenuItems
{
    public sealed class MenuItemQueryResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public short Type { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
