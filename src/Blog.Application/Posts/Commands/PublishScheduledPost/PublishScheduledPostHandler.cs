using Blog.Application.Abstractions.Commands;
using Blog.Domain.Abstractions;
using Blog.Domain.Core;
using Blog.Domain.Primitives;
using Blog.Domain.Primitives.Errors;

namespace Blog.Application.Posts.Commands.PublishScheduledPost;

internal sealed class PublishScheduledPostHandler : ICommandHandler<PublishScheduledPostCommand, Result>
{
  private readonly IRepository<BlogPost> _postRepository;
  private readonly IRepository<PostSchedule> _postScheduleRepository;
  private readonly IUnitOfWork _unitOfWork;

  public PublishScheduledPostHandler(
    IRepository<BlogPost> repository,
    IRepository<PostSchedule> postScheduleRepository,
    IUnitOfWork unitOfWork)
  {
    _postRepository = repository;
    _postScheduleRepository = postScheduleRepository;
    _unitOfWork = unitOfWork;
  }
  public async Task<Result> Handle(PublishScheduledPostCommand request, CancellationToken cancellationToken)
  {
    // GETTING POST
    var post = await _postRepository.TryFindAsync(post => post.Id == request.PostId, cancellationToken: cancellationToken);

    // VALIDATING
    if (post is null)
    {
      return Result.Failure(BlogPostErrors.NotFound);
    }

    if (post.IsPublished)
    {
      return Result.Failure(BlogPostErrors.AlreadyPosted);
    }

    // PUBLISHING
    post.Publish();

    // GETTING SCHEDULE
    var postSchedule = await _postScheduleRepository.TryFindAsync(postSchedule => postSchedule.BlogPostId == post.Id, cancellationToken: cancellationToken);

    // VALIDATING
    if (postSchedule is null)
    {
      return Result.Failure(PostScheduleErrors.NotFound);
    }

    if (postSchedule.Completed)
    {
      return Result.Failure(PostScheduleErrors.AlreadyCompleted);
    }

    // COMPLETING
    postSchedule.Complete();

    await _unitOfWork.SaveChangesAsync();

    return Result.Succeeded();
  }
}