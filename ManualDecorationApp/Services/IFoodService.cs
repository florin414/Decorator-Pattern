using ManualDecorationApp.Models;
namespace ManualDecoration.Services;
public interface IFoodService
{
    Task<ProductsResponse?> GetGroceryProductsAsync(string query, int number);
}