using ManualDecoration.Services;
using ManualDecorationApp.Models;
using Polly;
using Polly.Contrib.WaitAndRetry;
using System.Net;

namespace ManualDecorationApp.Services;

public class RetryPolicySpoonacularFoodService : IFoodService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IConfiguration configuration;
    private readonly IAsyncPolicy<HttpResponseMessage> retryPolicy =
        Policy<HttpResponseMessage>
        .Handle<HttpRequestException>()
        .OrResult(x => x.StatusCode >= HttpStatusCode.InternalServerError || x.StatusCode == HttpStatusCode.RequestTimeout)
        .WaitAndRetryAsync(
            Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)
         );

    public RetryPolicySpoonacularFoodService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<ProductsResponse?> GetGroceryProductsAsync(string query, int number)
    {
        var client = this.httpClientFactory.CreateClient();

        string apiKey = this.configuration["SpoonacularFoodApiKey"];

        var url = $"https://api.spoonacular.com/food/products/search?query={query}&number={number}&apiKey={apiKey}";

        var foodResponse = await retryPolicy.ExecuteAsync(() => client.GetAsync(url));

        var food = await foodResponse.Content.ReadFromJsonAsync<ProductsResponse?>();

        return food;
    }
}