using Blog.Application.Abstractions.Queries;
using Blog.Application.Posts.Dtos;
using Blog.Domain.Primitives;

namespace Blog.Application.Posts.Queries.GetPostsTree;

public sealed record GetPostsTreeQuery() : IQuery<Result<PostsTreeDto>>;