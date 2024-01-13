using MediatR;

namespace Blog.Application.Abstractions.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
  
}