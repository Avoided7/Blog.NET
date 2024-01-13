using Blog.Application.Abstractions.Queries;
using Blog.Domain.Core;
using Blog.Domain.Primitives;

namespace Blog.Application.Posts.Queries.GetPostById;

public sealed record GetPostByIdQuery(string Id) : IQuery<Result<BlogPost>>;