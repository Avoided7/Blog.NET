using Blog.Application.Abstractions.Queries;
using Blog.Application.Posts.Dtos;
using Blog.Domain.Abstractions;
using Blog.Domain.Core;
using Blog.Domain.Primitives;

namespace Blog.Application.Posts.Queries.GetPostsTree;

internal sealed class GetPostsTreeHandler : IQueryHandler<GetPostsTreeQuery, Result<PostsTreeDto>>
{
  private readonly IRepository<BlogPost> _blogPostRepository;

  public GetPostsTreeHandler(IRepository<BlogPost> blogPostRepository)
  {
    _blogPostRepository = blogPostRepository;
  }

  public Task<Result<PostsTreeDto>> Handle(GetPostsTreeQuery request, CancellationToken cancellationToken)
  {
    var posts = _blogPostRepository
      .Get(noTracking: true)
      .Where(post => post.IsPublished)
      .OrderByDescending(post => post.PublishedAt);

    var nodes = new List<PostTreeNodeDto>();

    foreach (var post in posts)
    {
      nodes.Add(new PostTreeNodeDto(post.Id, post.Title, post.PublishedAt!.Value));
    }

    var tree = new PostsTreeDto(nodes);

    return Task.FromResult(Result.Succeeded(tree));
  }
}