using Blog.Domain.Primitives;

namespace Blog.Domain.Core;

public sealed class BlogPost : Entity
{
  public string Title { get; private set; } = default!;
  public string ShortDescription { get; private set; } = default!;
  public string Content { get; private set; } = default!;
  public string ImagePath { get; private set; } = default!;
  public string Tags { get; private set; } = default!;
  public DateTime CreatedAt { get; private set; }
  public DateTime? PublishedAt { get; private set; }
  public bool IsPublished { get; private set; }

  private BlogPost() { }

  public static BlogPost Create(
    string title,
    string shortDescription,
    string content,
    string imagePath,
    string tags,
    DateTime createdAt,
    DateTime? publishedAt = null,
    bool isPublished = false)
  {
    if (isPublished && publishedAt is null)
    {
      throw new ArgumentException("Post cannot be published without date.");
    }

    if (!isPublished && publishedAt is not null)
    {
      throw new ArgumentException("Not published post cannot have publish date.");
    }

    return new BlogPost
    {
      Id = Guid.NewGuid().ToString(),
      Title = title,
      ShortDescription = shortDescription,
      Content = content,
      ImagePath = imagePath,
      Tags = tags,
      CreatedAt = createdAt,
      PublishedAt = publishedAt,
      IsPublished = isPublished
    };
  }

  public void Publish()
  {
    if (IsPublished)
    {
      throw new InvalidOperationException("Cannot publish already published post.");
    }

    IsPublished = true;
    PublishedAt = DateTime.Now;
  }
}