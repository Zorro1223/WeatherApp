using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeatherService _weatherService = new WeatherService();

        public async Task<IActionResult> Index(string city = "Москва", string period = "week")
        {
            try
            {
                var weatherItems = await _weatherService.GetWeatherAsync(city, period);
                var model = new WeatherViewModel
                {
                    City = city,
                    Forecast = weatherItems,
                    Period = period
                };
                return View(model);
            }
            catch
            {
                return View(new WeatherViewModel { Error = "Город не найден или ошибка API", City = city });
            }
        }
    }
}