using MediatR;

namespace Blog.Application.Abstractions.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
  where TQuery : IRequest<TResponse>
  where TResponse : class
{
  
}