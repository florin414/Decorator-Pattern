namespace ManualDecorationApp.Models;

public class Product
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("imageType")]
    public string ImageType { get; set; }
}
