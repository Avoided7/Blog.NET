using Blog.Application.Abstractions.Queries;
using Blog.Domain.Core;
using Blog.Domain.Primitives;

namespace Blog.Application.Posts.Queries.GetLastPublishedPosts;

public sealed record GetLastPublishedPostsQuery(int Page) : IQuery<Result<IEnumerable<BlogPost>>>;