using System;

namespace GroceryPointOfSale.Domain
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
