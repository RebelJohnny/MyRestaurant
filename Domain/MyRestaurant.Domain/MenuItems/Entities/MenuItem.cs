using MyRestaurant.Domain.MenuItems.Args;
using MyRestaurant.Domain.MenuItems.Enums;
using MyRestaurant.Domain.Shared.Abstracts;

namespace MyRestaurant.Domain.MenuItems.Entities
{
    public class MenuItem : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public MenuItemTypeEnum Type { get; private set; }
        public byte[] RowVersion { get; private set; }
        public bool IsDeleted { get; private set; }
        private MenuItem() { }
        private MenuItem(MenuItemArgs args)
        {
            Id = args.Id;
            Name = args.Name;
            Type = args.Type;
        }
        public static MenuItem Create(MenuItemArgs args)
        {
            return new MenuItem(args);
        }
        public void Modify(MenuItemArgs args)
        {
            args.Name = Name;
            args.Type = Type;
        }
        public void SoftDelete()
        {
            IsDeleted = true;
        }
    }
}
