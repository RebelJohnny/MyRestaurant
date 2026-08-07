using MyRestaurant.Domain.Menus.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

namespace MyRestaurant.Domain.Menus.Entities
{
    public class Menu : AuditableEntity, IAggregateRoot
    {
        public DateTimeOffset Date { get; private set; }
        private List<MenuArticle> _articles = [];
        public IReadOnlyCollection<MenuArticle> Articles => _articles;
        public byte[] RowVersion { get; private set; }
        private Menu() { }
        private Menu(ITimestampIdGenerator idGenerator, MenuArgs args)
        {
            Id = idGenerator.NextId();
            Date = args.Date;
            SetArticles(idGenerator, args.Articles);
        }
        public static Menu Create(ITimestampIdGenerator idGenerator, MenuArgs args)
        {
            return new Menu(idGenerator, args);
        }
        public void SetArticles(ITimestampIdGenerator idGenerator, List<MenuArticleArgs> args)
        {
            var articles = args.Select(a => MenuArticle.Create(idGenerator, a)).ToList();
            _articles = articles;
        }
    }
}
