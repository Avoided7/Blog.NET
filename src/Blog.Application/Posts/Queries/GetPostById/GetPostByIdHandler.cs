using Blog.Application.Abstractions.Queries;
using Blog.Domain.Abstractions;
using Blog.Domain.Core;
using Blog.Domain.Primitives;
using Blog.Domain.Primitives.Errors;

namespace Blog.Application.Posts.Queries.GetPostById;

internal sealed class GetPostByIdHandler : IQueryHandler<GetPostByIdQuery, Result<BlogPost>>
{
  private readonly IRepository<BlogPost> _blogPostRepository;

  public GetPostByIdHandler(IRepository<BlogPost> blogPostRepository)
  {
    _blogPostRepository = blogPostRepository;
  }

  public async Task<Result<BlogPost>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
  {
    var post = await _blogPostRepository.TryFindAsync(post => post.Id == request.Id, noTracking: true, cancellationToken: cancellationToken);

    if (post is null)
    {
      return Result.Failure<BlogPost>(BlogPostErrors.NotFound);
    }

    return Result.Succeeded(post);
  }
}