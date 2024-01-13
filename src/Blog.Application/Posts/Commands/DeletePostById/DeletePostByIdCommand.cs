using Blog.Application.Abstractions.Commands;
using Blog.Domain.Primitives;

namespace Blog.Application.Posts.Commands.DeletePostById;

public sealed record DeletePostByIdCommand(string PostId) : ICommand<Result>;