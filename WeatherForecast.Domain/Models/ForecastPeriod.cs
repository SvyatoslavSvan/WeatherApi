namespace WeatherForecast.Domain.Models
{
    public sealed class ForecastPeriod
    {
        private DateTime _to;

        private DateTime _from;

        public ForecastPeriod(DateTime to)
        {
            To = to;
            From = DateTime.Now.Date;
        }

        public ForecastPeriod(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public static DateTime LowerBound => DateTime.Today.AddDays(-92);

        public static DateTime UpperBound => DateTime.Today.AddDays(16);

        public DateTime From
        {
            get => _from;
            set
            {
                if (value < LowerBound)
                    throw new ArgumentException("Cannot give temperature state lower than 92 days from today");
                _from = value;
            }
        }

        public DateTime To
        {
            get => _to;
            set
            {
                if (value > UpperBound)
                    throw new ArgumentException("Cannot give temperature state upper than 16 days from today");
                _to = value;
            }
        }

    }
}
