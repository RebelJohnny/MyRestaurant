using MyRestaurant.Application.Contracts.Personnels;
using MyRestaurant.Domain.Personnels;
using MyRestaurant.Domain.Personnels.Entities;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Exceptions;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Personnels
{
    internal class PersonnelCommandHandler(ITimestampIdGenerator idGenerator, IUnitOfWork unitOfWork, IPersonnelRepository repository, IPersonnelDomainService domainService) :
        ICommandHandler<CreatePersonnelCommand, Result<PersonnelDTO>>,
        ICommandHandler<UpdatePersonnelCommand, Result>,
        ICommandHandler<DeletePersonnelCommand>,
        ICommandHandler<ReserveForPersonnelCommand, Result>
    {
        public async Task<Result<PersonnelDTO>> Handle(CreatePersonnelCommand request, CancellationToken cancellationToken)
        {
            var args = PersonnelMapper.Map(request);
            var result = await Personnel.Create(idGenerator, args, domainService, cancellationToken);
            if (!result.IsSuccess)
            {
                return Result<PersonnelDTO>.Failure(result.Error!);
            }
            var personnel = result.Value!;
            await repository.Add(personnel);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var returnType = PersonnelMapper.Map(personnel);
            return Result<PersonnelDTO>.Success(returnType);
        }

        public async Task<Result> Handle(UpdatePersonnelCommand request, CancellationToken cancellationToken)
        {
            var personnel = await repository.GetById(request.Id, cancellationToken) ?? throw Error.NotFound;
            var args = PersonnelMapper.Map(request);
            var result = await personnel.Modify(args, domainService, cancellationToken);
            if (result.IsSuccess)
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return result;
        }

        public async Task Handle(DeletePersonnelCommand request, CancellationToken cancellationToken)
        {
            var personnel = await repository.GetById(request.Id, cancellationToken) ?? throw Error.NotFound;
            personnel.SoftDelete();
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<Result> Handle(ReserveForPersonnelCommand request, CancellationToken cancellationToken)
        {
            var personnel = await repository.GetById(request.PersonnelId, cancellationToken) ?? throw Error.NotFound;
            var args = PersonnelMapper.Map(request);
            var result = await personnel.ReserveOrders(idGenerator, args, domainService, cancellationToken);
            if (result.IsSuccess)
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return result;
        }
    }
}
