using MediatR;

namespace MyRestaurant.Framework.Mediator
{
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
    }    
}
