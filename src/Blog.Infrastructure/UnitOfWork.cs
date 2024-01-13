using Blog.Domain.Abstractions;

namespace Blog.Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
  private readonly ApplicationDbContext _dbContext;

  public UnitOfWork(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public void SaveChanges()
  {
    _dbContext.SaveChanges();
  }

  public async Task SaveChangesAsync()
  {
    await _dbContext.SaveChangesAsync();
  }
}