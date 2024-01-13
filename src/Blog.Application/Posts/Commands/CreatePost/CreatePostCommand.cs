using Blog.Application.Abstractions.Commands;
using Blog.Application.Posts.Dtos;
using Blog.Domain.Core;
using Blog.Domain.Primitives;

namespace Blog.Application.Posts.Commands.CreatePost;

public sealed record CreatePostCommand(BlogPostDto PostDto) : ICommand<Result<BlogPost>>;