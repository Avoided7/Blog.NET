namespace Blog.Domain.Primitives.Errors;

public static class PostScheduleErrors
{
  public static Error UncompletedNotFound { get; } = new Error("PostSchedule.NotFound", "Uncompleted post schedule(s) cannot be found.");
  public static Error NotFound { get; } = new Error("PostSchedule.NotFound", "Schedule cannot be found.");
  public static Error AlreadyCompleted { get; } = new Error("PostSchedule.AlreadyCompleted", "Post schedule cannot complete twice.");
  public static Error CreatingInPast { get; } = new Error("PostSchedule.CreatingInPast", "Cannot schedule post in the past.");
}