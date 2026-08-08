using MyRestaurant.Application.Contracts.Personnels;
using MyRestaurant.Domain.Personnels;
using MyRestaurant.Domain.Personnels.Entities;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Personnels
{
    internal class PersonnelCommandHandler(ITimestampIdGenerator idGenerator, IUnitOfWork unitOfWork, IPersonnelRepository repository) :
        ICommandHandler<CreatePersonnelCommand, PersonnelDTO>,
        ICommandHandler<UpdatePersonnelCommand>,
        ICommandHandler<DeletePersonnelCommand>,
        ICommandHandler<ReserveForPersonnelCommand>
    {
        public async Task<PersonnelDTO> Handle(CreatePersonnelCommand request, CancellationToken cancellationToken)
        {
            var args = PersonnelMapper.Map(request);
            var personnel = Personnel.Create(idGenerator, args);
            await repository.Add(personnel);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return PersonnelMapper.Map(personnel);
        }

        public async Task Handle(UpdatePersonnelCommand request, CancellationToken cancellationToken)
        {
            var personnel = await repository.GetById(request.Id, cancellationToken);
            var args = PersonnelMapper.Map(request);
            personnel.Modify(args);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task Handle(DeletePersonnelCommand request, CancellationToken cancellationToken)
        {
            var personnel = await repository.GetById(request.Id, cancellationToken);
            personnel.SoftDelete();
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task Handle(ReserveForPersonnelCommand request, CancellationToken cancellationToken)
        {
            var personnel = await repository.GetById(request.PersonnelId, cancellationToken);
            var args = PersonnelMapper.Map(request);
            personnel.ReserveOrders(idGenerator, args);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
