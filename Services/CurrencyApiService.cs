using SmartGarage.Models;

namespace SmartGarage.Services;
public class CurrencyApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _baseUrl;

    public CurrencyApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["CurrencyApi:ApiKey"];
        _baseUrl = configuration["CurrencyApi:BaseUrl"];
    }

    public async Task<CurrencyApiResponse> GetExchangeRatesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<CurrencyApiResponse>($"{_baseUrl}latest?apikey={_apiKey}");
        return response;
    }
}
