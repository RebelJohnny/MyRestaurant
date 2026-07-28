using MyRestaurant.Domain.MealPeriods.Args;
using MyRestaurant.Domain.Shared.Abstracts;

namespace MyRestaurant.Domain.MealPeriods.Entities
{
    public class MealPeriod : Entity
    {
        public string Name { get; private set; }
        public int Time { get; private set; }
        public bool IsActive { get; private set; }
        public byte[] RowVersion { get; private set; }
        public bool IsDeleted { get; private set; }
        private MealPeriod() { }
        private MealPeriod(MealPeriodArgs args)
        {
            Id = args.Id;
            Name = args.Name;
            Time = args.Time;
        }
        public static MealPeriod Create(MealPeriodArgs args)
        {
            return new MealPeriod(args);
        }
        public void Modify(MealPeriodArgs args)
        {
            Id = args.Id;
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
