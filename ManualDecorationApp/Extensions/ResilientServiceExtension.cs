namespace ManualDecorationApp.Extensions;

internal static class ResilientServiceExtension
{
    public static IServiceCollection AddResilient<TConcreteService, TService, TResilientService>(
        this IServiceCollection services
    ) where TConcreteService : class, TService
      where TResilientService : class, TService
    {
        Type interfaceType = typeof(TService);
        Type serviceType = typeof(TConcreteService);

        if (!interfaceType.IsAssignableFrom(serviceType))
        {
            throw new ArgumentException($"{serviceType.FullName} must implement {interfaceType.FullName}");
        }

        services.AddSingleton<TConcreteService>();

        services.AddSingleton(typeof(TService), provider => ActivatorUtilities.CreateInstance<TResilientService>(provider));

        return services;
    }
}
