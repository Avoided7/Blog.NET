using MediatR;

namespace Blog.Application.Abstractions.Commands;

public interface ICommand : IRequest
{
  
}

public interface ICommand<out T> : IRequest<T>
  where T : class
{

}