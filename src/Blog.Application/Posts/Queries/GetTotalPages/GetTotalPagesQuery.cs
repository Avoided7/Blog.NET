using Blog.Application.Abstractions.Queries;
using Blog.Domain.Primitives;

namespace Blog.Application.Posts.Queries.GetTotalPages;

public sealed record GetTotalPagesQuery() : IQuery<Result<int>>;