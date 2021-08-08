using System.Net.Http;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HelooDotnet5
{
    public class WeatherClient
    {
        private readonly HttpClient httpClient;
        private readonly ServiceSettings serviceSettings;
        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            this.httpClient = httpClient;
            this.serviceSettings = options.Value;

        }
        public record Weather(string description);
        public record Main(decimal temp);
        public record Forecast(Weather[] weather, Main Main, long dt);
        public async Task<Forecast> GetCurrentWeather(string city)
        {
            var forecast = await httpClient.GetFromJsonAsync<Forecast>($"https://{serviceSettings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={serviceSettings.ApiKey}&units=metric");
            return forecast;
        }
    }
}