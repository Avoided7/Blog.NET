using Blog.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddDbContextInMemory(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemory"));

    return serviceCollection;
  }

  public static IServiceCollection AddRepository(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));

    return serviceCollection;
  }

  public static IServiceCollection AddUnitOfWork(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

    return serviceCollection;
  }
}