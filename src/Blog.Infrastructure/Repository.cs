using System.Linq.Expressions;
using Blog.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure;

internal sealed class Repository<T> : IRepository<T> 
  where T : class
{
  private readonly DbSet<T> _set;

  public Repository(ApplicationDbContext dbContext)
  {
    _set = dbContext.Set<T>();
  }

  public IQueryable<T> Get(bool noTracking = true)
  {
    return GetSet(noTracking);
  }

  public IQueryable<T> Get(Expression<Func<T, bool>> expression, bool noTracking = true)
  {
    var set = GetSet(noTracking);

    return set.Where(expression);
  }

  public async Task<T> FindAsync(Expression<Func<T, bool>> expression, bool noTracking = true, CancellationToken cancellationToken = default)
  {
    var set = GetSet(noTracking);

    return await set.FirstAsync(expression, cancellationToken);
  }

  public async Task<T?> TryFindAsync(Expression<Func<T, bool>> expression, bool noTracking = true, CancellationToken cancellationToken = default)
  {
    var set = GetSet(noTracking);

    return await set.FirstAsync(expression, cancellationToken);
  }

  public void Create(T entity)
  {
    _set.Entry(entity).State = EntityState.Added;
  }

  public void Update(T entity)
  {
    _set.Entry(entity).State = EntityState.Modified;
  }

  public void Delete(T entity)
  {
    _set.Entry(entity).State = EntityState.Deleted;
  }

  private IQueryable<T> GetSet(bool noTracking)
  {
    if (noTracking)
    {
      return _set.AsNoTracking();
    }

    return _set;
  }
}