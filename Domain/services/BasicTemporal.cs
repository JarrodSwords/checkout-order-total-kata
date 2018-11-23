using System;

namespace PointOfSale.Domain
{
    public class Temporal : ITemporal
    {
        public DateTime EndTime { get; }
        public DateTime StartTime { get; }

        public bool IsActive
        {
            get
            {
                var now = DateTime.Now;
                return now >= StartTime && now <= EndTime;
            }
        }

        public Temporal(DateTime endTime, DateTime startTime)
        {
            EndTime = endTime;
            StartTime = startTime;
        }
    }
}
