using MyRestaurant.Domain.Meals.Args;
using MyRestaurant.Domain.Meals.Enums;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

namespace MyRestaurant.Domain.Meals.Entities
{
    public sealed class Meal : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public MealTypeEnum Type { get; private set; }
        public byte[] RowVersion { get; private set; }
        public bool IsDeleted { get; private set; }
        private Meal() { }
        private Meal(ITimestampIdGenerator idGenerator, MealArgs args)
        {
            Id = idGenerator.NextId();
            Name = args.Name;
            Type = args.Type;
        }
        public static Meal Create(ITimestampIdGenerator idGenerator, MealArgs args)
        {
            return new Meal(idGenerator, args);
        }
        public void Modify(MealArgs args)
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
