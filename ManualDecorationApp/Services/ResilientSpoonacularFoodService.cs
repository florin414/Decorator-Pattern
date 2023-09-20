namespace ManualDecorationApp.Services;

public class ResilientSpoonacularFoodService : IFoodService
{
    private readonly IFoodService foodService;

    public ResilientSpoonacularFoodService(IFoodService foodService)
    {
        this.foodService = foodService ?? throw new ArgumentNullException(nameof(foodService));
    }

    public async Task<ProductsResponse?> GetGroceryProductsByQueryAndNumberAsync(string query, int number)
    {
        var retryCount = 0;
        Start:
        try
        {
            return await foodService.GetGroceryProductsByQueryAndNumberAsync(query, number);
        }
        catch (Exception)
        {
            if (retryCount <= 4)
            {
                retryCount++;
                goto Start;
            }
            throw;
        }
    }
}
