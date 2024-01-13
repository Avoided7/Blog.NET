using MediatR;

namespace Blog.Application.Abstractions.Commands;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
  where TCommand : ICommand<TResponse>
  where TResponse : class
{
  
}

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
  where TCommand : ICommand
{

}