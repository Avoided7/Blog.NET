namespace Blog.Domain.Primitives;

public class Entity : Entity<string>
{

}

public class Entity<T>
{
  public T Id { get; protected set; } = default!;
}