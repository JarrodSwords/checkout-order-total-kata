using System;

namespace GroceryPointOfSale.Domain
{
    public class BasicTemporal : ITemporal
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

        public BasicTemporal(DateTime endTime, DateTime startTime)
        {
            EndTime = endTime;
            StartTime = startTime;
        }
    }
}
