using MediatR;

namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public class GetPersonnelFormDataQuery : IRequest<PersonnelFormData>
    {
        public long Id { get; set; }
    }
}
