using ManualDecorationApp.Models;
using System.Net;

namespace ManualDecoration.Services;
public class SpoonacularFoodService : IFoodService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IConfiguration configuration;

    public SpoonacularFoodService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<ProductsResponse?> GetGroceryProductsAsync(string query, int number)
    {
        var client = this.httpClientFactory.CreateClient();

        string apiKey = this.configuration["SpoonacularFoodApiKey"];

        var url = $"https://api.spoonacular.com/food/products/search?query={query}&number={number}&apiKey={apiKey}";

        var foodResponse = await client.GetAsync(url);

        if (foodResponse.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        var food = await foodResponse.Content.ReadFromJsonAsync<ProductsResponse?>();

        return food;
    }
}