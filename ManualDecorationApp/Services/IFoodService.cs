namespace ManualDecoration.Services;

public interface IFoodService
{
    Task<ProductsResponse?> GetGroceryProductsByQueryAndNumberAsync(string query, int number);
}
