using Blog.Application.Abstractions.Commands;
using Blog.Domain.Primitives;

namespace Blog.Application.Posts.Commands.PublishScheduledPost;

public sealed record PublishScheduledPostCommand(string PostId) : ICommand<Result>;