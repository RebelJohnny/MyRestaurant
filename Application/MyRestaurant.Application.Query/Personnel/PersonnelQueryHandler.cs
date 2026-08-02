using MyRestaurant.Application.Query.Contracts.Personnels;
using MyRestaurant.EF.Read.Repositories.Personnels;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Personnel
{
    internal class PersonnelQueryHandler(IPersonnelQueryRepository repository) :
        IQueryHandler<GetPersonnelFormDataQuery, PersonnelFormData>,
        IQueryHandler<GetPersonnelQuery, IEnumerable<PersonnelQueryResult>>
    {
        public async Task<PersonnelFormData> Handle(GetPersonnelFormDataQuery request, CancellationToken cancellationToken)
        {
            var personnel = await repository.GetById(request.Id, cancellationToken);
            return personnel;
        }

        public async Task<IEnumerable<PersonnelQueryResult>> Handle(GetPersonnelQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAll(cancellationToken);
        }
    }
}
