using static System.Runtime.InteropServices.JavaScript.JSType;

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
                    throw new ArgumentException($"Date 'From' cannot be earlier than {LowerBound.ToShortDateString()}.", nameof(From));
                _from = value;
            }
        }

        public DateTime To
        {
            get => _to;
            set
            {
                if (value > UpperBound)
                    throw new ArgumentException($"Date 'To' cannot be later than {UpperBound.ToShortDateString()}.", nameof(To));
                _to = value;
            }
        }

    }
}
