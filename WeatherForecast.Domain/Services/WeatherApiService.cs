using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Web;
using WeatherForecast.Domain.DTO.Forecast;
using WeatherForecast.Domain.DTO.SearchCity;
using WeatherForecast.Domain.Exceptions;
using WeatherForecast.Domain.Interfaces;
using WeatherForecast.Domain.Models;


namespace WeatherForecast.Domain.Services
{
    public class WeatherApiService : IWeatherApiService
    {
        private readonly HttpClient _httpClient = new();
        
        public async Task<TemperatureState> GetTodayTemperatureState(TimeOnly hour, string cityName)
        {
            UriBuilder builder = new()
            {
                Scheme = "https",
                Host = "api.open-meteo.com/v1/forecast"
            };
            var city = await GetCitiesByName(cityName);
            CreateTodayTemperatureRequest(city.First(), builder);
            var response = await _httpClient.GetAsync(builder.Uri);
            ProceedResponse(response);
            var forecast = await response.Content.ReadFromJsonAsync<ForecastResponse>();
            var index = forecast.Hourly.Time.FindIndex(x => x.TimeOfDay.Hours == hour.ToTimeSpan().Hours);
            if (index != -1)
            {
                var temperature = forecast.Hourly.Temperature_2m[index];
                return new TemperatureState(new DateTime(DateOnly.FromDateTime(DateTime.Now.Date), hour), temperature);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        private static void ProceedResponse(HttpResponseMessage response)
        {
            if (response.StatusCode is HttpStatusCode.BadRequest)
            {
                throw new ArgumentException(HttpStatusCode.BadRequest.ToString());
            }
            if (response.StatusCode is HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException(HttpStatusCode.InternalServerError.ToString());
            }
            if (response.StatusCode is HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }

        }

        private void CreateTodayTemperatureRequest(City city, UriBuilder builder)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query[nameof(city.Latitude).ToLower()] = city.Latitude.ToString(CultureInfo.InvariantCulture);
            query[nameof(city.Longitude).ToLower()] = city.Longitude.ToString(CultureInfo.InvariantCulture); 
            query["temperature_unit"] = "celsius";
            query["hourly"] = "temperature_2m";
            query["forecast_days"] = "1";
            builder.Query = query.ToString();
        }

        public async Task<IList<TemperatureState>> GetForecast(ForecastPeriod forecastPeriod, string cityName)
        {
            UriBuilder builder = new()
            {
                Scheme = "https",
                Host = "api.open-meteo.com/v1/forecast"
            };
            var city = await GetCitiesByName(cityName);
            CreateForecastRequest(forecastPeriod, city.First(), builder);
            var response = await _httpClient.GetAsync(builder.Uri);
            ProceedResponse(response);
            var forecast = await response.Content.ReadFromJsonAsync<ForecastResponse>() ?? throw new NullReferenceException();
            return forecast.Hourly.ToTemperatureStateCollection();
        }

        private void CreateForecastRequest(ForecastPeriod forecastPeriod, City city, UriBuilder builder)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query[nameof(city.Latitude).ToLower()] = city.Latitude.ToString(CultureInfo.InvariantCulture);
            query[nameof(city.Longitude).ToLower()] = city.Longitude.ToString(CultureInfo.InvariantCulture);
            query["temperature_unit"] = "celsius";
            query["hourly"] = "temperature_2m";
            query["start_date"] = forecastPeriod.From.Date.ToString("yyyy-MM-dd");
            query["end_date"] = forecastPeriod.To.Date.ToString("yyyy-MM-dd");
            builder.Query = query.ToString();
        }

        public async Task<IList<City>> GetCitiesByName(string name, int count = 1)
        {
            UriBuilder builder = new()
            {
                Scheme = "https",
                Host = "geocoding-api.open-meteo.com/v1/search"
            };
            CreateCityRequest(name, count, builder);
            var response = await _httpClient.GetAsync(builder.Uri);
            ProceedResponse(response);
            var cities = await response.Content.ReadFromJsonAsync<CityRequest>() ?? throw new NullReferenceException();
            return cities.ToCityCollection();
        }

        private void CreateCityRequest(string name, int count, UriBuilder builder)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["name"] = name;
            query["count"] = count.ToString();
            query["language"] = "en";
            query["format"] = "json";
            builder.Query = query.ToString();
        }
    }
}
