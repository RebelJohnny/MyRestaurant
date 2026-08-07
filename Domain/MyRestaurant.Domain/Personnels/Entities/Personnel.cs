using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

namespace MyRestaurant.Domain.Personnels.Entities
{
    public class Personnel : AuditableEntity, IAggregateRoot
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        private List<PersonnelReservedOrder> _reservedOrders = [];
        public IReadOnlyCollection<PersonnelReservedOrder> ReservedOrders => _reservedOrders;
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
        public void ReserveOrders(ITimestampIdGenerator idGenerator, PersonnelReservedOrderArgs args)
        {
            var reserve = ReservedOrders.FirstOrDefault(ro => ro.Date == args.Date);
            if (reserve is null)
            {
                reserve = PersonnelReservedOrder.Create(idGenerator, args);
                reserve.SetArticles(idGenerator, args.Articles);
                _reservedOrders.Add(reserve);
            }
            else
            {
                reserve.SetArticles(idGenerator, args.Articles);
            }
        }
        public void SoftDelete()
        {
            IsDeleted = true;
        }
    }
}
