using MyRestaurant.Application.Contracts.Personnels;
using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Personnels.Entities;

namespace MyRestaurant.Application.Personnels
{
    internal static class PersonnelMapper
    {
        internal static PersonnelDTO Map(Personnel personnel)
        {
            return new PersonnelDTO(personnel.Id, personnel.Code, personnel.Name);
        }
        internal static PersonnelArgs Map(CreatePersonnelCommand command)
        {
            return new PersonnelArgs
            {
                Code = command.Code,
                Name = command.Name
            };
        }
        internal static PersonnelArgs Map(UpdatePersonnelCommand command)
        {
            return new PersonnelArgs
            {
                Code = command.Code,
                Name = command.Name
            };
        }
        internal static PersonnelReserveArgs Map(ReserveForPersonnelCommand command)
        {
            return new PersonnelReserveArgs
            {
                Date = command.Date,
                Meals = [.. command.Meals.Select(a => new PersonnelReservedMealArgs
                {
                    Id = a.Id,
                    MealPeriodId = a.MealPeriodId,
                    Count = a.Count
                })]
            };
        }
    }
}
