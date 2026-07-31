using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public class GetPersonnelFormDataQuery : IQuery<PersonnelFormData>
    {
        public long Id { get; set; }
    }
}
