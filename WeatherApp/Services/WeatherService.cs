using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly string _apiKey = "bb79c9bd2bb05c51090e225e6e3d06d8";

        public async Task<List<WeatherData>> GetWeatherAsync(string city, string period)
        {
            using var client = new HttpClient();
            // Для примера используем 5-дневный прогноз (бесплатный лимит)
            string url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={_apiKey}&units=metric&lang=ru";

            var response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<dynamic>(response);

            var forecastList = new List<WeatherData>();

            foreach (var item in data.list)
            {
                forecastList.Add(new WeatherData
                {
                    City = data.city.name,
                    Temperature = (float)item.main.temp,
                    Description = (string)item.weather[0].description,
                    Icon = (string)item.weather[0].icon,
                    Humidity = (float)item.main.humidity,
                    WindSpeed = (float)item.wind.speed,
                    Date = DateTime.Parse((string)item.dt_txt)
                });
            }

            // Фильтрация в зависимости от выбранного периода
            if (period == "day")
                return forecastList.Take(8).ToList(); // Каждые 3 часа на 1 день

            return forecastList.Where(x => x.Date.Hour == 12).ToList(); // По одному замеру в полдень на неделю
        }
    }
}