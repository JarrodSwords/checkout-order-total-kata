using System;

namespace PointOfSale.Domain
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
