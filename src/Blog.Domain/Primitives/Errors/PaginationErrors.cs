namespace Blog.Domain.Primitives.Errors;

public static class PaginationErrors
{
  public static Error IncorrectPage { get; } = new Error("Pagination.IncorrectPage", "Entities with this page cannot be found.");
}