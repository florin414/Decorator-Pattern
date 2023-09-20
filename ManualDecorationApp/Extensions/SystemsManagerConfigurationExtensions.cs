namespace ManualDecorationApp.Extensions;

public static class SystemsManagerConfigurationExtensions
{
    public static IConfigurationBuilder AddAmazonSystemsManager(
        this IConfigurationBuilder builder,
        IWebHostEnvironment environment
    )
    {
        var env = environment.EnvironmentName.ToLower();
        builder.AddSystemsManager($"/{env}/spoonacularfood-api");

        return builder;
    }
}
