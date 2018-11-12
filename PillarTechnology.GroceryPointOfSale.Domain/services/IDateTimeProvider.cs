using System;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
