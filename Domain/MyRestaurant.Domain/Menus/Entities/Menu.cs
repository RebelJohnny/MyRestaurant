using MyRestaurant.Domain.Menus.Args;
using MyRestaurant.Domain.Shared.Abstracts;

namespace MyRestaurant.Domain.Menus.Entities
{
    public class Menu : AuditableEntity, IAggregateRoot
    {
        public DateTimeOffset Date { get; private set; }
        private List<MenuArticle> _articles = [];
        public IReadOnlyCollection<MenuArticle> Articles => _articles;
        public byte[] RowVersion { get; private set; }
        private Menu() { }
        private Menu(MenuArgs args)
        {
            Id = args.Id;
            Date = args.Date;
        }
        public static Menu Create(MenuArgs args)
        {
            return new Menu(args);
        }
        public void SetArticles(List<MenuArticleArgs> args)
        {
            var articles = args.Select(MenuArticle.Create).ToList();
            _articles = articles;
        }
    }
}
