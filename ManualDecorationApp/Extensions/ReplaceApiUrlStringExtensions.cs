namespace ManualDecorationApp.Extensions;

public static class ReplaceApiUrlStringExtensions
{
    public static string ReplaceApiUrlWithValues(
        this string apiUrl,
        string query,
        int number,
        string apiKey
    )
    {
        string apiUrlWithValues = apiUrl
            .Replace("{query}", query)
            .Replace("{number}", number.ToString())
            .Replace("{apiKey}", apiKey);

        return apiUrlWithValues;
    }
}
