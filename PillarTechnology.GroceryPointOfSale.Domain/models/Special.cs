using System;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public abstract class Special
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ScannedItemsRequired { get { return GetScannedItemsRequired(); } }

        public Special() { }

        public Special(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public abstract Money CalculateSalePrice(Product product);
        public abstract string GetLineItemDescription(Product product);
        public abstract int GetScannedItemsRequired();
    }
}
