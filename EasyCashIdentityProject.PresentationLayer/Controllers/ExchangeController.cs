using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class ExchangeController : Controller
    {
        private async Task<string> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://currency-converter241.p.rapidapi.com/conversion_rate?from={fromCurrency}&to={toCurrency}"),
                Headers =
                {
                    { "x-rapidapi-key", "1fe30d34d5msh281c09c486cd920p13a192jsn7c424be4ec08" },
                    { "x-rapidapi-host", "currency-converter241.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(body);
                var rate = json["rate"].Value<decimal>();

                // Formatlama işlemi
                return rate.ToString("F4");
            }
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.UsdToTry = await GetExchangeRateAsync("USD", "TRY");
            ViewBag.EurToTry = await GetExchangeRateAsync("EUR", "TRY");
            ViewBag.GbpToTry = await GetExchangeRateAsync("GBP", "TRY");
            ViewBag.EurToUsd = await GetExchangeRateAsync("USD", "EUR");
            ViewBag.UsdToEur = await GetExchangeRateAsync("USD", "EUR");
            ViewBag.UsdToGbp = await GetExchangeRateAsync("USD", "GBP");
            ViewBag.EurToGbp = await GetExchangeRateAsync("EUR", "GBP");
            ViewBag.UsdToJpy = await GetExchangeRateAsync("USD", "JPY");
            ViewBag.EurToJpy = await GetExchangeRateAsync("EUR", "JPY");
            ViewBag.GbpToJpy = await GetExchangeRateAsync("GBP", "JPY");
            ViewBag.UsdToCad = await GetExchangeRateAsync("USD", "CAD");
            ViewBag.EurToCad = await GetExchangeRateAsync("EUR", "CAD");
            ViewBag.GbpToCad = await GetExchangeRateAsync("GBP", "CAD");
            ViewBag.UsdToAud = await GetExchangeRateAsync("USD", "AUD");
            ViewBag.EurToAud = await GetExchangeRateAsync("EUR", "AUD");
            ViewBag.GbpToAud = await GetExchangeRateAsync("GBP", "AUD");

            return View();
        }
    }
}
