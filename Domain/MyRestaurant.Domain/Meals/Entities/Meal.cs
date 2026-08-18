using MyRestaurant.Domain.Meals.Args;
using MyRestaurant.Domain.Meals.Enums;
using MyRestaurant.Domain.Meals.Exceptions;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Exceptions;
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
        private Meal(long id, MealArgs args)
        {
            Id = id;
            Name = args.Name;
            Type = args.Type;
        }
        public static async Task<Result<Meal>> Create(ITimestampIdGenerator idGenerator, MealArgs args, IMealDomainService domainService, CancellationToken cancellationToken)
        {
            var id = idGenerator.NextId();
            var error = await Validate(id, args, domainService, cancellationToken);
            if (error is not null)
            {
                return Result<Meal>.Failure(error);
            }
            var meal = new Meal(id, args);
            return Result<Meal>.Success(meal);
        }
        public async Task<Result> Modify(MealArgs args, IMealDomainService domainService, CancellationToken cancellationToken)
        {
            var error = await Validate(Id, args, domainService, cancellationToken);
            if (error is not null)
            {
                return Result.Failure(error);
            }
            Name = args.Name;
            Type = args.Type;
            return Result.Success();
        }
        public void SoftDelete()
        {
            IsDeleted = true;
        }
        private static async Task<Error?> Validate(long id, MealArgs args, IMealDomainService domainService, CancellationToken cancellationToken)
        {
            return
                GuardAgainstEmptyName(args.Name) ??
                GuardAgainstEmptyMealType(args.Type) ??
                await GuardAgainstExistingMealName(id, args.Name, domainService, cancellationToken);
        }
        private static Error? GuardAgainstEmptyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return MealExceptions.MealNameRequired;
            }
            return null;
        }
        private static Error? GuardAgainstEmptyMealType(MealTypeEnum type)
        {
            if (type == MealTypeEnum.None)
            {
                return MealExceptions.MealTypeRequired;
            }
            return null;
        }
        private static async Task<Error?> GuardAgainstExistingMealName(long id, string name, IMealDomainService domainService, CancellationToken cancellationToken)
        {
            if (await domainService.CheckNameExistence(id, name, cancellationToken))
            {
                return MealExceptions.MealNameExists;
            }
            return null;
        }
    }
}
