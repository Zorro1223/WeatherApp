namespace WeatherApp.Models
{
    public class WeatherData
    {
        public string City { get; set; }
        public float Temperature { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public float Humidity { get; set; }
        public float WindSpeed { get; set; }
        public DateTime Date { get; set; }
    }

    public class WeatherViewModel
    {
        public string City { get; set; }
        public List<WeatherData> Forecast { get; set; }
        public string Error { get; set; }
        public string Period { get; set; } // "day", "week", "month"
    }
}