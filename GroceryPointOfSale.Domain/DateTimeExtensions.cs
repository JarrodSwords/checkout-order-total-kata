using System;

namespace GroceryPointOfSale.Domain
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime date)
        {
            var diff = (7 + (date.DayOfWeek - DayOfWeek.Sunday)) % 7;
            return date.AddDays(-diff).Date;
        }

        public static DateTime StartOfLastWeek(this DateTime date) => date.StartOfWeek().AddDays(-7);
        public static DateTime StartOfNextWeek(this DateTime date) => date.StartOfWeek().AddDays(7);
        public static DateTime EndOfWeek(this DateTime date) => date.StartOfWeek().AddDays(7).AddSeconds(-1);
        public static DateTime EndOfLastWeek(this DateTime date) => date.StartOfWeek().AddSeconds(-1);
        public static DateTime EndOfNextWeek(this DateTime date) => date.StartOfWeek().AddDays(14).AddSeconds(-1);
    }
}