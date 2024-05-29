using Microsoft.AspNetCore.Mvc;
using SmartGarage.Services;

namespace SmartGarage.Controllers;
public class HomeController : Controller
{
    private readonly CurrencyApiService _currencyApiService;

    public HomeController(CurrencyApiService currencyApiService)
    {
        _currencyApiService = currencyApiService;
    }

    public async Task<IActionResult> Index()
    {
        var exchangeRates = await _currencyApiService.GetExchangeRatesAsync();
        return View(exchangeRates);
    }
}
