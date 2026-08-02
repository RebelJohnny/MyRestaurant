using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public sealed class GetPersonnelFormDataQuery : IQuery<PersonnelFormData>
    {
        public long Id { get; set; }
    }
}
