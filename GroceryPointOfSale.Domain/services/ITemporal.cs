using System;

namespace GroceryPointOfSale.Domain
{
    public interface ITemporal
    {
        DateTime EndTime { get; }
        bool IsActive { get; }
        DateTime StartTime { get; }
    }
}
