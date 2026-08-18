using MyRestaurant.Domain.MealPeriods.Args;
using MyRestaurant.Domain.MealPeriods.Exceptions;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Exceptions;
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
        private MealPeriod(long id, MealPeriodArgs args)
        {
            Id = id;
            Name = args.Name;
            Time = args.Time;
            IsActive = true;
        }
        public static async Task<Result<MealPeriod>> Create(ITimestampIdGenerator idGenerator, MealPeriodArgs args, IMealPeriodDomainService domainService, CancellationToken cancellationToken)
        {
            var id = idGenerator.NextId();
            var error = await Validate(id, args, domainService, cancellationToken);
            if (error is not null)
            {
                return Result<MealPeriod>.Failure(error);
            }
            var mealPeriod = new MealPeriod(id, args);
            return Result<MealPeriod>.Success(mealPeriod);
        }
        public async Task<Result> Modify(MealPeriodArgs args, IMealPeriodDomainService domainService, CancellationToken cancellationToken)
        {
            var error = await Validate(Id, args, domainService, cancellationToken);
            if (error is not null)
            {
                return Result.Failure(error);
            }
            Name = args.Name;
            Time = args.Time;
            return Result.Success();
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
        private static async Task<Error?> Validate(long id, MealPeriodArgs args, IMealPeriodDomainService domainService, CancellationToken cancellationToken)
        {
            return
                GuardAgainstEmptyName(args.Name) ??
                GuardAgainstInvalidTime(args.Time) ??
                await GuardAgainstExistingName(id, args.Name, domainService, cancellationToken);
        }
        private static Error? GuardAgainstEmptyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return MealPeriodExceptions.MealPeriodNameRequired;
            }
            return null;
        }
        private static Error? GuardAgainstInvalidTime(int time)
        {
            if (time < 0 || time > 86400)
            {
                return MealPeriodExceptions.MealPeriodInvalidTime;
            }
            return null;
        }
        private static async Task<Error?> GuardAgainstExistingName(long id, string name, IMealPeriodDomainService domainService, CancellationToken cancellationToken)
        {
            if (await domainService.CheckNameExistence(id, name, cancellationToken))
            {
                return MealPeriodExceptions.MealPeriodNameExists;
            }
            return null;
        }
    }
}
