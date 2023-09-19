namespace ManualDecorationApp.Models;

using Newtonsoft.Json;

public class Product
{
    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("title")]  public string Title { get; set; }

    [JsonProperty("imageType")] public string ImageType { get; set; }
}
