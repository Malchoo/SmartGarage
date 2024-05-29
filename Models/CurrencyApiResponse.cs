namespace SmartGarage.Models;
public class CurrencyApiResponse
{
    public bool Success { get; set; }
    public Dictionary<string, decimal> Rates { get; set; }
    public string Base { get; set; }
    public DateTime Date { get; set; }
}
