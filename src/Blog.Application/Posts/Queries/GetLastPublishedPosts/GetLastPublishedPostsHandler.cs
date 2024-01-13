using Blog.Application.Abstractions.Queries;
using Blog.Domain.Abstractions;
using Blog.Domain.Core;
using Blog.Domain.Primitives;
using Blog.Domain.Primitives.Errors;

namespace Blog.Application.Posts.Queries.GetLastPublishedPosts;

internal sealed class GetLastPublishedPostsHandler : IQueryHandler<GetLastPublishedPostsQuery, Result<IEnumerable<BlogPost>>>
{
  private readonly IRepository<BlogPost> _blogPostRepository;

  public GetLastPublishedPostsHandler(IRepository<BlogPost> blogPostRepository)
  {
    _blogPostRepository = blogPostRepository;
  }

  public Task<Result<IEnumerable<BlogPost>>> Handle(GetLastPublishedPostsQuery request, CancellationToken cancellationToken)
  {
    var posts = _blogPostRepository
      .Get(post => post.IsPublished, noTracking: true)
      .OrderByDescending(post => post.PublishedAt)
      .Skip((request.Page - 1) * PostsConstants.PAGE_SIZE)
      .Take(PostsConstants.PAGE_SIZE);

    if (!posts.Any())
    {
      return Task.FromResult(Result.Failure<IEnumerable<BlogPost>>(PaginationErrors.IncorrectPage));
    }

    return Task.FromResult(Result.Succeeded((IEnumerable<BlogPost>) posts));
  }
}