using Blog.Domain.Primitives;

namespace Blog.Domain.Core;

public sealed class PostSchedule : Entity
{
  public string BlogPostId { get; private set; } = default!;
  public DateTime Schedule { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public bool Completed { get; private set; }

  private PostSchedule() { }

  public static PostSchedule Create(
    string blogPostId,
    DateTime schedule,
    DateTime createdAt,
    bool completed = false)
  {
    return new PostSchedule
    {
      Id = Guid.NewGuid().ToString(),
      BlogPostId = blogPostId,
      Schedule = schedule,
      CreatedAt = createdAt,
      Completed = completed
    };
  }

  public void Complete()
  {
    Completed = true;
  }
}