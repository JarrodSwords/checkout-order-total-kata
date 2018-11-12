using System;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public interface ITemporal
    {
        DateTime EndTime { get; }
        bool IsActive { get; }
        DateTime StartTime { get; }
    }
}
