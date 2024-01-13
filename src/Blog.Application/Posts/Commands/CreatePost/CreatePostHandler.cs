using Blog.Application.Abstractions.Commands;
using Blog.Domain.Abstractions;
using Blog.Domain.Core;
using Blog.Domain.Primitives;
using Blog.Domain.Primitives.Errors;

namespace Blog.Application.Posts.Commands.CreatePost;

internal sealed class CreatePostHandler : ICommandHandler<CreatePostCommand, Result<BlogPost>>
{
  private readonly IRepository<BlogPost> _blogPostRepository;
  private readonly IRepository<PostSchedule> _postScheduleRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreatePostHandler(
    IRepository<BlogPost> blogPostRepository,
    IRepository<PostSchedule> postScheduleRepository,
    IUnitOfWork unitOfWork)
  {
    _blogPostRepository = blogPostRepository;
    _postScheduleRepository = postScheduleRepository;
    _unitOfWork = unitOfWork;
  }
  public async Task<Result<BlogPost>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
  {
    var dtoPost = request.PostDto;

    if (dtoPost is { IsPublished: true, Schedule: not null })
    {
      // Published and scheduled.
      return Result.Failure<BlogPost>(BlogPostErrors.PublishedWithSchedule);
    }

    var now = DateTime.Now;

    var post = BlogPost.Create(
      dtoPost.Title,
      dtoPost.ShortDescription,
      dtoPost.Content,
      dtoPost.ImagePath,
      dtoPost.Tags,
      now,
      dtoPost.IsPublished ? now : null,
      dtoPost.IsPublished);

    if (dtoPost.Schedule.HasValue)
    {
      if (dtoPost.Schedule < now)
      {
        // Scheduled in the past.
        return Result.Failure<BlogPost>(PostScheduleErrors.CreatingInPast);
      }

      var schedule = PostSchedule.Create(
        post.Id,
        dtoPost.Schedule.Value,
        now,
        false);

      _postScheduleRepository.Create(schedule);
    }

    _blogPostRepository.Create(post);

    await _unitOfWork.SaveChangesAsync();

    return Result.Succeeded(post);
  }
}