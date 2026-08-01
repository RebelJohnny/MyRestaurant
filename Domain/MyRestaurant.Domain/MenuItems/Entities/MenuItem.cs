using MyRestaurant.Domain.MenuItems.Args;
using MyRestaurant.Domain.MenuItems.Enums;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

namespace MyRestaurant.Domain.MenuItems.Entities
{
    public sealed class MenuItem : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public MenuItemTypeEnum Type { get; private set; }
        public byte[] RowVersion { get; private set; }
        public bool IsDeleted { get; private set; }
        private MenuItem() { }
        private MenuItem(ITimestampIdGenerator idGenerator, MenuItemArgs args)
        {
            Id = idGenerator.NextId();
            Name = args.Name;
            Type = args.Type;
        }
        public static MenuItem Create(ITimestampIdGenerator idGenerator, MenuItemArgs args)
        {
            return new MenuItem(idGenerator, args);
        }
        public void Modify(MenuItemArgs args)
        {
            Name = args.Name;
            Type = args.Type;
        }
        public void SoftDelete()
        {
            IsDeleted = true;
        }
    }
}
