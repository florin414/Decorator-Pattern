namespace ManualDecorationApp.Services;

public class RetryPolicySpoonacularFoodService : IFoodService
{
    private readonly IHttpClientFactory httpClientFactory;
    private SpoonacularFoodApiSettings SpoonacularFoodApiSettings { get; set; }
    private readonly IAsyncPolicy<HttpResponseMessage> retryPolicy = Policy<HttpResponseMessage>
        .Handle<HttpRequestException>()
        .OrResult(
            x =>
                x.StatusCode >= HttpStatusCode.InternalServerError
                || x.StatusCode == HttpStatusCode.RequestTimeout
        )
        .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5));

    public RetryPolicySpoonacularFoodService(
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

        var foodResponse = await retryPolicy.ExecuteAsync(() => client.GetAsync(apiUrlWithValues));

        var food = await foodResponse.Content.ReadFromJsonAsync<ProductsResponse?>();

        return food;
    }
}
