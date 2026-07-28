namespace MyRestaurant.Domain.Menus.Args
{
    public class MenuArgs
    {
        public long Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<MenuArticleArgs> Articles { get; set; }
    }
}
