using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddApplicationMediatR(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddMediatR(options =>
    {
      options.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
    });

    return serviceCollection;
  }
}