using System;

namespace PointOfSale.Domain
{
    public abstract class SpecialFactory : ISpecialFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        protected DateTime EndTime { get; set; }
        protected int? Limit { get; set; }
        protected DateTime StartTime { get; set; }

        protected SpecialFactory(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public void Configure(DateTime endTime, DateTime startTime, int? limit = null)
        {
            EndTime = endTime;
            Limit = limit;
            StartTime = startTime;
        }

        public abstract Special CreateSpecial();
    }
}
