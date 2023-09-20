namespace ManualDecoration.Services;

public class SpoonacularFoodService : IFoodService
{
    private readonly IHttpClientFactory httpClientFactory;
    private SpoonacularFoodApiSettings SpoonacularFoodApiSettings { get; set; }

    public SpoonacularFoodService(
        IHttpClientFactory httpClientFactory,
        IOptions<SpoonacularFoodApiSettings> options
    )
    {
        this.httpClientFactory =
            httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        this.SpoonacularFoodApiSettings = options.Value;
    }

    public async Task<ProductsResponse?> GetGroceryProductsByQueryAndNumberAsync(
        string query,
        int number
    )
    {
        var client = this.httpClientFactory.CreateClient();
        var apiKey = this.SpoonacularFoodApiSettings.ApiKey;
        var apiUrl = this.SpoonacularFoodApiSettings.ApiUrl;

        var apiUrlWithValues = apiUrl.ReplaceApiUrlWithValues(query, number, apiKey);

        var foodResponse = await client.GetAsync(apiUrlWithValues);

        if (foodResponse.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        var food = await foodResponse.Content.ReadFromJsonAsync<ProductsResponse?>();

        return food;
    }
}
