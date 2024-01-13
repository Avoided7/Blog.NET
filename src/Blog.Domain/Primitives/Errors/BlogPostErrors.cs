namespace Blog.Domain.Primitives.Errors;

public static class BlogPostErrors
{
  public static Error NotFound { get; } = new Error("BlogPost.NotFound", "Blog post cannot be found.");
  public static Error AlreadyPosted { get; } = new Error("BlogPost.AlreadyPosted", "Blog post cannot be posted twice.");
  public static Error PublishedWithSchedule { get; } = new Error("BlogPost.PublishedWithSchedule", "Cannot create published blog post with schedule.");
}