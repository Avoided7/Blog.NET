using Blog.Application.Abstractions.Queries;
using Blog.Domain.Abstractions;
using Blog.Domain.Core;
using Blog.Domain.Primitives;
using Blog.Domain.Primitives.Errors;

namespace Blog.Application.PostsSchedules.Queries.GetUncompletedSchedules;

internal sealed class GetUncompletedSchedulesHandler : IQueryHandler<GetUncompletedSchedulesQuery, Result<IEnumerable<PostSchedule>>>
{
  private readonly IRepository<PostSchedule> _repository;

  public GetUncompletedSchedulesHandler(IRepository<PostSchedule> repository)
  {
    _repository = repository;
  }

  public Task<Result<IEnumerable<PostSchedule>>> Handle(GetUncompletedSchedulesQuery request, CancellationToken cancellationToken)
  {
    var now = DateTime.Now;
    var postSchedules = _repository
      .Get(postSchedule => !postSchedule.Completed && now >= postSchedule.Schedule, noTracking: true)
      .Take(request.Size);

    if (!postSchedules.Any())
    {
      return Task.FromResult(Result.Failure<IEnumerable<PostSchedule>>(PostScheduleErrors.UncompletedNotFound));
    }

    return Task.FromResult(Result.Succeeded((IEnumerable<PostSchedule>)postSchedules));
  }
}