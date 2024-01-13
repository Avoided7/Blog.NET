using Blog.Application.Abstractions.Commands;
using Blog.Domain.Abstractions;
using Blog.Domain.Core;
using Blog.Domain.Primitives;
using Blog.Domain.Primitives.Errors;

namespace Blog.Application.Posts.Commands.DeletePostById;

internal sealed class DeletePostByIdHandler : ICommandHandler<DeletePostByIdCommand, Result>
{
  private readonly IRepository<BlogPost> _blogPostRepository;
  private readonly IRepository<PostSchedule> _postScheduleRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeletePostByIdHandler(
    IRepository<BlogPost> blogPostRepository,
    IRepository<PostSchedule> postScheduleRepository,
    IUnitOfWork unitOfWork)
  {
    _blogPostRepository = blogPostRepository;
    _postScheduleRepository = postScheduleRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result> Handle(DeletePostByIdCommand request, CancellationToken cancellationToken)
  {
    var post = await _blogPostRepository.TryFindAsync(post => post.Id == request.PostId, cancellationToken: cancellationToken);

    if (post is null)
    {
      return Result.Failure(BlogPostErrors.NotFound);
    }

    _blogPostRepository.Delete(post);

    if (!post.IsPublished)
    {
      var postSchedule = await _postScheduleRepository.TryFindAsync(postSchedule => postSchedule.BlogPostId == post.Id, cancellationToken: cancellationToken);

      if (postSchedule is not null)
      {
        _postScheduleRepository.Delete(postSchedule);
      }
    }

    await _unitOfWork.SaveChangesAsync();

    return Result.Succeeded();
  }
}