using MediatR;

namespace MyRestaurant.Framework.Mediator
{
    public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
    }
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    {
    }
}
