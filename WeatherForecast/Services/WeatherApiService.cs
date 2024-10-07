using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Web;
using WeatherForecast.Domain.DTO.Forecast;
using WeatherForecast.Domain.DTO.SearchCity;
using WeatherForecast.Domain.Exceptions;
using WeatherForecast.Domain.Models;
using WeatherForecast.Domain.Services.Interfaces;


namespace WeatherForecast.Domain.Services
{
    public sealed class WeatherApiService : IWeatherApiService
    {
        private readonly HttpClient _httpClient;

        public WeatherApiService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<TemperatureState> GetTodayTemperatureState(TimeOnly hour, string cityName)
        {
            UriBuilder builder = new()
            {
                Scheme = "https",
                Host = "api.open-meteo.com/v1/forecast"
            };

            var city = (await GetCitiesByName(cityName)).First();
            CreateTodayTemperatureRequest(city, builder);

            var response = await _httpClient.GetAsync(builder.Uri);
            ProceedResponse(response);

            var forecast = await response.Content.ReadFromJsonAsync<ForecastResponse>();

            if (forecast is null)
            {
                throw new NullReferenceException("Forecast response is null");
            }

            var index = forecast.Hourly.Time.FindIndex(x => x.TimeOfDay.Hours == hour.ToTimeSpan().Hours);

            if (index == -1)
            {
                throw new IndexOutOfRangeException("The specified hour was not found in the forecast.");
            }

            var temperature = forecast.Hourly.Temperature_2m[index];
            return new TemperatureState(new DateTime(DateOnly.FromDateTime(DateTime.Now.Date), hour), temperature);
        }

        public async Task<IList<TemperatureState>> GetForecast(ForecastPeriod forecastPeriod, string cityName)
        {
            UriBuilder builder = new()
            {
                Scheme = "https",
                Host = "api.open-meteo.com/v1/forecast"
            };

            var city = (await GetCitiesByName(cityName)).First();
            CreateForecastRequest(forecastPeriod, city, builder);

            var response = await _httpClient.GetAsync(builder.Uri);
            ProceedResponse(response);

            var forecast = await response.Content.ReadFromJsonAsync<ForecastResponse>()
                           ?? throw new NullReferenceException("Forecast response is null");

            return forecast.Hourly.ToTemperatureStateCollection();
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

            var cities = await response.Content.ReadFromJsonAsync<CityRequest>()
                         ?? throw new NullReferenceException("City response is null");

            return cities.ToCityCollection();
        }

        private static void ProceedResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new ArgumentException("Bad request.");
                    case HttpStatusCode.InternalServerError:
                        throw new HttpRequestException("Internal server error.");
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException();
                    default:
                        throw new HttpRequestException($"Unexpected HTTP status code: {response.StatusCode}");
                }
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
