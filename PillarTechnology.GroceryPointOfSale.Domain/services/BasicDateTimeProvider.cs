using System;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class BasicDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
