using MediatR;

namespace MyRestaurant.Framework.Mediator
{
    public interface ICommand<TResult> : IRequest<TResult>
    {
    }
    public interface ICommand : IRequest
    {
    }
}
