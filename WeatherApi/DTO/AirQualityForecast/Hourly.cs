// ReSharper disable InconsistentNaming

namespace WeatherForecast.DTO.AirQualityForecast
{
    public class Hourly
    {
        public Hourly(IList<DateTime> time,
            IList<float> pm10,
            IList<float> pm25,
            IList<float> carbonMonoxide,
            IList<float> nitrogenDioxide,
            IList<float> ozone, List<DateTime> time1, List<int> europeanAqiPm10, List<int> europeanAqiPm25, List<int> europeanAqiNitrogenDioxide, List<int> europeanAqiOzone, List<int> europeanAqiSulphurDioxide)
        {
            this.time = time1;
            european_aqi_pm10 = europeanAqiPm10;
            european_aqi_pm2_5 = europeanAqiPm25;
            european_aqi_nitrogen_dioxide = europeanAqiNitrogenDioxide;
            european_aqi_ozone = europeanAqiOzone;
            european_aqi_sulphur_dioxide = europeanAqiSulphurDioxide;
        }

        public List<DateTime> time { get; set; }
        public List<int> european_aqi_pm10 { get; set; }
        public List<int> european_aqi_pm2_5 { get; set; }
        public List<int> european_aqi_nitrogen_dioxide { get; set; }
        public List<int> european_aqi_ozone { get; set; }
        public List<int> european_aqi_sulphur_dioxide { get; set; }

        public List<AirQuality> ToAirQualityCollection(string cityName)
        {
            var airQualities = new List<AirQuality>();
            for (int i = 0; i < time.Count; i++)
            {
                airQualities.Add(CreateAirQuality(cityName, i));
            }
            return airQualities;
        }

        public AirQuality CreateAirQuality(string cityName, int index) => new(time[index], european_aqi_pm10[index], european_aqi_pm2_5[index],
            european_aqi_nitrogen_dioxide[index], european_aqi_ozone[index], european_aqi_sulphur_dioxide[index], cityName);
    }
}
