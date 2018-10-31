using System;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public static class DateTimeProvider
    {
        public static DateTime Now() => new DateTime(2018, 10, 30);

        public static DateTime StartOfWeek(this DateTime date)
        {
            var diff = (7 + (date.DayOfWeek - DayOfWeek.Sunday)) % 7;
            return date.AddDays(-diff).Date;
        }
        
        public static DateTime StartOfLastWeek(this DateTime date) => date.StartOfWeek().AddDays(-7).Date;
        public static DateTime StartOfNextWeek(this DateTime date) => date.StartOfWeek().AddDays(7).Date;
        public static DateTime EndOfWeek(this DateTime date) => date.StartOfWeek().AddDays(6).Date;
        public static DateTime EndOfLastWeek(this DateTime date) => date.StartOfWeek().AddDays(-1).Date;
        public static DateTime EndOfNextWeek(this DateTime date) => date.StartOfWeek().AddDays(13).Date;
    }
}