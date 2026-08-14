using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Personnels.Exceptions;
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
        private Personnel(long id, PersonnelArgs args)
        {
            Id = id;
            Code = args.Code;
            Name = args.Name;
        }
        public static async Task<Personnel> Create(ITimestampIdGenerator idGenerator, PersonnelArgs args, IPersonnelDomainService domainService)
        {
            var id = idGenerator.NextId();
            await GuardAgainstCodeExistence(id, args.Code, domainService);
            return new Personnel(id, args);
        }
        public async Task Modify(PersonnelArgs args, IPersonnelDomainService domainService)
        {
            await GuardAgainstCodeExistence(Id, args.Code, domainService);
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
        private static async Task GuardAgainstCodeExistence(long id, string code, IPersonnelDomainService domainService)
        {
            if (await domainService.CheckCodeExistence(id, code))
            {
                throw PersonnelExceptions.PersonnelCodeExists;
            }
        }
    }
}
