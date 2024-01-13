namespace Blog.Domain.Primitives;

public sealed record Error(string Code, string Description)
{
  public static Error Empty { get; } = new Error(string.Empty, string.Empty);
}