using System.Globalization;
using System.Net.Http.Json;
using System.Web;
using WeatherForecast.Domain.DTO;
using WeatherForecast.Domain.Interfaces;
using WeatherForecast.Domain.Models;

namespace WeatherForecast.Domain.Services
{
    public class WeatherApiService : IWeatherApiService
    {
        private readonly UriBuilder _uriBuilder = new UriBuilder()
        {
            Scheme = "https",
            Host = "api.open-meteo.com/v1/forecast"
        };

        private readonly HttpClient _httpClient = new HttpClient();
        
        public async Task<TemperatureState> GetTodayTemperatureState(TimeOnly hour, City city)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query[nameof(city.Latitude).ToLower()] = city.Latitude.ToString(CultureInfo.InvariantCulture);
            query[nameof(city.Longitude).ToLower()] = city.Longitude.ToString(CultureInfo.InvariantCulture); 
            query["temperature_unit"] = "celsius";
            query["hourly"] = "temperature_2m";
            _uriBuilder.Query = query.ToString();
            var response = await _httpClient.GetAsync(_uriBuilder.Uri);
            if (response.IsSuccessStatusCode)
            {
                var forecast = await response.Content.ReadFromJsonAsync<ForecastResponse>();
                var index = forecast.Hourly.Time.FindIndex(x => x.TimeOfDay.Hours == hour.ToTimeSpan().Hours);
                if (index != -1)
                {
                    var temperature = forecast.Hourly.Temperature_2m[index];
                    return new TemperatureState(new DateTime(), temperature);
                }
            }
            throw new HttpRequestException(response.StatusCode.ToString());
        }

        public TemperatureState GetForecast(Period period, City city)
        {
            throw new NotImplementedException();
        }
    }
}
