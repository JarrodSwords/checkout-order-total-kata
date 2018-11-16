using System;

namespace PointOfSale.Domain
{
    public class BasicDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
