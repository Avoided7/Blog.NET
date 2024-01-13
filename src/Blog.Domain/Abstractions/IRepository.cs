using System.Linq.Expressions;

namespace Blog.Domain.Abstractions;

public interface IRepository<T> 
  where T : class
{
  IQueryable<T> Get(bool noTracking = false);
  IQueryable<T> Get(Expression<Func<T, bool>> expression, bool noTracking = false);

  Task<T> FindAsync(Expression<Func<T, bool>> expression, bool noTracking = false, CancellationToken cancellationToken = default);
  Task<T?> TryFindAsync(Expression<Func<T, bool>> expression, bool noTracking = false, CancellationToken cancellationToken = default);

  void Create(T entity);
  void Update(T entity);
  void Delete(T entity);
}