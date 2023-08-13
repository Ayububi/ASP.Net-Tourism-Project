using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Tour_FP.Services
{
    public class WeatherService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public WeatherService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _apiKey = configuration["OpenWeatherMap:ApiKey"]; // Use your API key from appsettings.json
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<string> GetWeatherAsync(string cityName)
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={_apiKey}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                dynamic data = JObject.Parse(json);

                string description = data.weather[0].description;
                double temperature = data.main.temp;
                int humidity = data.main.humidity;
                double windSpeed = data.wind.speed;

                return $"Weather in {cityName}: {description}, Temperature: {temperature - 273.15:F2}°C, Humidity: {humidity}%, Wind Speed: {windSpeed} m/s";
            }
                return "Weather information not available.";
        }
    }
}
