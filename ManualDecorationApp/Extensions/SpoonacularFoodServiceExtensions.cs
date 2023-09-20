namespace ManualDecorationApp.Extensions;

public static class SpoonacularFoodServiceExtensions
{
    public static IServiceCollection AddSpoonacularFoodService(this IServiceCollection services)
    {
        //services.AddSingleton<IFoodService, RetryPolicySpoonacularFoodService>();

        //services.AddSingleton<SpoonacularFoodService>();
        //services.AddSingleton<IFoodService>(x =>
        //    new ResilientSpoonacularFoodService(x.GetRequiredService<SpoonacularFoodService>()));

        services.AddResilient<
            SpoonacularFoodService,
            IFoodService,
            ResilientSpoonacularFoodService
        >();

        return services;
    }

    public static WebApplication MapSpoonacularFoodApi(this WebApplication app)
    {
        app.MapGet("/foods", GetGroceryProductsByQueryAndNumberAsync)
            .WithTags("GroceryProductsByQueryAndNumber");

        return app;
    }

    private static async Task<IResult> GetGroceryProductsByQueryAndNumberAsync(
        string query,
        int number,
        IFoodService foodService
    )
    {
        var foods = await foodService.GetGroceryProductsByQueryAndNumberAsync(query, number);
        return foods != null ? Results.Ok(foods) : Results.NotFound();
    }
}
