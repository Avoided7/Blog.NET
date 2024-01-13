using System.Text.Json;
using Blog.Application.Posts.Commands.PublishScheduledPost;
using Blog.Application.PostsSchedules.Queries.GetUncompletedSchedules;
using Blog.Domain.Core;
using Blog.Domain.Primitives.Errors;
using MediatR;

namespace Blog.Api.BackgroundServices;

public sealed class PostPublisher : BackgroundService
{
  private const int HANDLE_PER_TIME = 5;

  private static TimeSpan WaitTime { get; } = TimeSpan.FromMinutes(1.5);

  private readonly IServiceScopeFactory _serviceScopeFactory;
  private readonly ILogger<PostPublisher> _logger;

  public PostPublisher(IServiceScopeFactory serviceScopeFactory, ILogger<PostPublisher> logger)
  {
    _serviceScopeFactory = serviceScopeFactory;
    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var request = new GetUncompletedSchedulesQuery(HANDLE_PER_TIME);

    while (!stoppingToken.IsCancellationRequested)
    {
      await Task.Delay(WaitTime, stoppingToken);

      // CREATING SCOPE
      await using var scope = _serviceScopeFactory.CreateAsyncScope();

      var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

      // GETTING POSTS
      var result = await mediator.Send(request, stoppingToken);
      
      // IF NO POSTS, CONTINUE
      if (!result.IsSucceeded)
      {
        if (result.Error == PostScheduleErrors.UncompletedNotFound)
        {
          continue;
        }

        throw new Exception(JsonSerializer.Serialize(result.Error));
      }

      await HandlePostsAsync(result.Data, mediator, stoppingToken);
    }
  }

  private async Task HandlePostsAsync(
    IEnumerable<PostSchedule> postsSchedules, 
    IMediator mediator, 
    CancellationToken cancellationToken = default)
  {
    var count = 0;
    var errors = 0;

    // PUBLISH POST AND COMPLETE SCHEDULE
    foreach (var postSchedule in postsSchedules)
    {
      var command = new PublishScheduledPostCommand(postSchedule.BlogPostId);

      var result = await mediator.Send(command, cancellationToken);

      if (!result.IsSucceeded)
      {
        _logger.LogError("{Code}: {Description}", result.Error.Code, result.Error.Description);
        errors++;
      }
      count++;
    }

    _logger.Log(LogLevel.Information, "Handled {PostsCount} schedules. Errors: {ErrorsCount}", count, errors);
  }
}