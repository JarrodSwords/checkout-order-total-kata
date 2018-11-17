using System;
using PointOfSale.Domain;

namespace PointOfSale.Test.Domain
{
    public enum DateRange
    {
        Active,
        Expired,
        Future
    }

    public static class DateRangeExtensions
    {
        public static DateTime GetStart(this DateRange dateRange, DateTime now)
        {
            switch (dateRange)
            {
                case DateRange.Expired:
                    return now.StartOfLastWeek();
                case DateRange.Future:
                    return now.StartOfNextWeek();
                case DateRange.Active:
                default:
                    return now.StartOfWeek();
            }
        }

        public static DateTime GetEnd(this DateRange dateRange, DateTime now)
        {
            switch (dateRange)
            {
                case DateRange.Expired:
                    return now.EndOfLastWeek();
                case DateRange.Future:
                    return now.EndOfNextWeek();
                case DateRange.Active:
                default:
                    return now.EndOfWeek();
            }
        }
    }
}
