using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

namespace MyRestaurant.Domain.Personnels.Entities
{
    public class Personnel : AuditableEntity, IAggregateRoot
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        private List<PersonnelReserve> _reserves = [];
        public IReadOnlyCollection<PersonnelReserve> Reserves => _reserves;
        public byte[] RowVersion { get; private set; }
        public bool IsDeleted { get; private set; }
        private Personnel() { }
        private Personnel(ITimestampIdGenerator idGenerator, PersonnelArgs args)
        {
            Id = idGenerator.NextId();
            Code = args.Code;
            Name = args.Name;
        }
        public static Personnel Create(ITimestampIdGenerator idGenerator, PersonnelArgs args)
        {
            return new Personnel(idGenerator, args);
        }
        public void Modify(PersonnelArgs args)
        {
            Code = args.Code;
            Name = args.Name;
        }
        public void ReserveOrders(ITimestampIdGenerator idGenerator, PersonnelReserveArgs args)
        {
            var reserve = Reserves.FirstOrDefault(ro => ro.Date == args.Date && ro.MealPeriodId == args.MealPeriodId);
            if (reserve is null)
            {
                reserve = PersonnelReserve.Create(idGenerator, args);
                reserve.SetArticles(args.Meals);
                _reserves.Add(reserve);
            }
            else
            {
                reserve.SetArticles(args.Meals);
            }
        }
        public void SoftDelete()
        {
            IsDeleted = true;
        }
    }
}
