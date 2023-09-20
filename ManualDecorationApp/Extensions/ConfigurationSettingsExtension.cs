namespace ManualDecorationApp.Extensions;

public static class ConfigurationSettingsExtension
{
    public static IServiceCollection AddConfig(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.Configure<SpoonacularFoodApiSettings>(
            config.GetSection(SpoonacularFoodApiSettings.SpoonacularFoodApi)
        );

        return services;
    }
}
