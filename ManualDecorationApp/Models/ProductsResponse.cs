namespace ManualDecorationApp.Models;

public class ProductsResponse
{
    [JsonProperty("products")]
    public List<Product> Products { get; set; }

    [JsonProperty("totalProducts")]
    public int TotalProducts { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("offset")]
    public int Offset { get; set; }

    [JsonProperty("number")]
    public int Number { get; set; }
}
