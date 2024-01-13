namespace Blog.Domain.Abstractions;

public interface IUnitOfWork
{
  void SaveChanges();
  Task SaveChangesAsync();
}