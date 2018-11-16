using System;

namespace GroceryPointOfSale.Domain
{
    public class BasicDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
