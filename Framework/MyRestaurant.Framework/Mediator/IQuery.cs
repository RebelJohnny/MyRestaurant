using MediatR;

namespace MyRestaurant.Framework.Mediator
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
