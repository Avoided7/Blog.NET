using Blog.Application.Abstractions.Queries;
using Blog.Domain.Abstractions;
using Blog.Domain.Core;
using Blog.Domain.Primitives;

namespace Blog.Application.Posts.Queries.GetTotalPages;

internal sealed class GetTotalPagesHandler : IQueryHandler<GetTotalPagesQuery, Result<int>>
{
  private readonly IRepository<BlogPost> _blogPostRepository;

  public GetTotalPagesHandler(IRepository<BlogPost> blogPostRepository)
  {
    _blogPostRepository = blogPostRepository;
  }

  public Task<Result<int>> Handle(GetTotalPagesQuery request, CancellationToken cancellationToken)
  {
    var posts = _blogPostRepository.Get(post => post.IsPublished, noTracking: true);
    var count = (int)Math.Ceiling(1f * posts.Count() / PostsConstants.PAGE_SIZE);

    return Task.FromResult(Result.Succeeded(count));
  }
}