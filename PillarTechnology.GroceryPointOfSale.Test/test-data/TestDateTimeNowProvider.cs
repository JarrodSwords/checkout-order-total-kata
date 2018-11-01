using System;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class TestDateTimeNowProvider : IDateTimeNowProvider
    {
        public DateTime Now() => DateTime.Now;
    }
}