using MyRestaurant.Domain.MealPeriods.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

namespace MyRestaurant.Domain.MealPeriods.Entities
{
    public sealed class MealPeriod : Entity
    {
        public string Name { get; private set; }
        public int Time { get; private set; }
        public bool IsActive { get; private set; }
        public byte[] RowVersion { get; private set; }
        public bool IsDeleted { get; private set; }
        private MealPeriod() { }
        private MealPeriod(ITimestampIdGenerator idGenerator, MealPeriodArgs args)
        {
            Id = idGenerator.NextId();
            Name = args.Name;
            Time = args.Time;
            IsActive = true;
        }
        public static MealPeriod Create(ITimestampIdGenerator idGenerator, MealPeriodArgs args)
        {
            return new MealPeriod(idGenerator, args);
        }
        public void Modify(MealPeriodArgs args)
        {
            Name = args.Name;
            Time = args.Time;
        }
        public void Activate()
        {
            IsActive = true;
        }
        public void Deactivate()
        {
            IsActive = false;
        }
        public void SoftDelete()
        {
            IsDeleted = true;
        }
    }
}
