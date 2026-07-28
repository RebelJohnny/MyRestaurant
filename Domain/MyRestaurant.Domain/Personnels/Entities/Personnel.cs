using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Shared.Abstracts;

namespace MyRestaurant.Domain.Personnels.Entities
{
    public class Personnel : Entity, IAggregateRoot
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        private List<PersonnelReservedOrder> _reservedOrders = [];
        public IReadOnlyCollection<PersonnelReservedOrder> ReservedOrders => _reservedOrders;
        public byte[] RowVersion { get; private set; }
        public bool IsDeleted { get; private set; }
        private Personnel() { }
        private Personnel(PersonnelArgs args)
        {
            Id = args.Id;
            Code = args.Code;
            Name = args.Name;
        }
        public static Personnel Create(PersonnelArgs args)
        {
            return new Personnel(args);
        }
        public void Modify(PersonnelArgs args)
        {
            Code = args.Code;
            Name = args.Name;
        }
        public void ReserveOrders(PersonnelReservedOrderArgs args)
        {
            var reserve = ReservedOrders.FirstOrDefault(ro => ro.Id == args.Id);
            if (reserve is null)
            {
                reserve = PersonnelReservedOrder.Create(args);
                reserve.SetArticles(args.Articles);
                _reservedOrders.Add(reserve);
            }
            else
            {
                reserve.SetArticles(args.Articles);
            }
        }
    }
}
