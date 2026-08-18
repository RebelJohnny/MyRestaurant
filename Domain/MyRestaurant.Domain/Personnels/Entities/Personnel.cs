using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Personnels.Exceptions;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Exceptions;
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
        public static async Task<Result<Personnel>> Create(ITimestampIdGenerator idGenerator, PersonnelArgs args, IPersonnelDomainService domainService, CancellationToken cancellationToken)
        {
            var id = idGenerator.NextId();
            var error = await Validate(id, args, domainService, cancellationToken);
            if (error is not null)
            {
                return Result<Personnel>.Failure(error);
            }
            var personnel = new Personnel(id, args);
            return Result<Personnel>.Success(personnel);
        }
        public async Task<Result> Modify(PersonnelArgs args, IPersonnelDomainService domainService, CancellationToken cancellationToken)
        {
            var error = await Validate(Id, args, domainService, cancellationToken);
            if (error is not null)
            {
                return Result.Failure(error);
            }
            Code = args.Code;
            Name = args.Name;
            return Result.Success();
        }
        public async Task<Result> ReserveOrders(ITimestampIdGenerator idGenerator, PersonnelReserveArgs args, IPersonnelDomainService domainService, CancellationToken cancellationToken)
        {
            var error = await ValidateReserve(args, domainService, cancellationToken);
            if (error is not null)
            {
                return Result.Failure(error);
            }
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
            return Result.Success();
        }
        public void SoftDelete()
        {
            IsDeleted = true;
        }
        private static async Task<Error?> Validate(long id, PersonnelArgs args, IPersonnelDomainService domainService, CancellationToken cancellationToken)
        {
            return
                GuardAgainstEmptyCode(args.Code) ??
                GuardAgainstEmptyName(args.Name) ??
                await GuardAgainstCodeExistence(id, args.Code, domainService, cancellationToken);
        }
        private static Error? GuardAgainstEmptyCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return PersonnelExceptions.PersonnelCodeRequired;
            }
            return null;
        }
        private static Error? GuardAgainstEmptyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return PersonnelExceptions.PersonnelNameRequired;
            }
            return null;
        }
        private static async Task<Error?> GuardAgainstCodeExistence(long id, string code, IPersonnelDomainService domainService, CancellationToken cancellationToken)
        {
            if (await domainService.CheckCodeExistence(id, code, cancellationToken))
            {
                return PersonnelExceptions.PersonnelCodeExists;
            }
            return null;
        }

        private static async Task<Error?> ValidateReserve(PersonnelReserveArgs args, IPersonnelDomainService domainService, CancellationToken cancellationToken)
        {
            return 
                GuardAgainstDateInThePast(args.Date) ?? 
                await GuardAgainstReservingMealsNotAvailableOnDayPeriod(args, domainService, cancellationToken);
        }
        private static Error? GuardAgainstDateInThePast(DateTimeOffset date)
        {
            if (date.Date.CompareTo(DateTimeOffset.Now.Date) < 0)
            {
                return PersonnelExceptions.ReserveDateInThePast;
            }
            return null;
        }
        private static async Task<Error?> GuardAgainstReservingMealsNotAvailableOnDayPeriod(PersonnelReserveArgs args, IPersonnelDomainService domainService, CancellationToken cancellationToken)
        {
            if (!await domainService.CheckMealsAvailableOnMenuForDayPeriod(args.Date, args.MealPeriodId, args.Meals.Select(m => m.Id), cancellationToken))
            {
                return PersonnelExceptions.MealsNotInMenuForDayPeriod;
            }
            return null;
        }
    }
}

 