﻿using WeatherForecast.Domain.Models;

namespace WeatherForecast.Domain.DTO.Forecast
{
    public sealed class Hourly
    {
        public List<DateTime> Time { get; set; } = null!;
        // ReSharper disable once InconsistentNaming
        public List<float> Temperature_2m { get; set; } = null!;

        public IList<TemperatureState> ToTemperatureStateCollection()
        {
            if (Time.Count != Temperature_2m.Count)
            {
                throw new ArgumentException("Time count and Temperature_2m is not equal");
            }
            var temperatureStates = new List<TemperatureState>();
            for (int i = 0; i < Time.Count; i++)
            {
                temperatureStates.Add(new TemperatureState(Time[i], Temperature_2m[i]));
            }
            return temperatureStates;
        }

    }
}
