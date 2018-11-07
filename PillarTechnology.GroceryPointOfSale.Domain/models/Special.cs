using System;
using System.Collections.Generic;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public abstract class Special
    {
        public abstract string Description { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public abstract int ScannedItemsRequired { get; }
        public int? Limit { get; }
        public bool IsActive
        {
            get
            {
                var now = DateTime.Now;
                return now >= StartTime && now <= EndTime;
            }
        }

        public Special() { }

        public Special(DateTime startTime, DateTime endTime, int? limit = null)
        {
            StartTime = startTime;
            EndTime = endTime;
            Limit = limit;
        }

        public abstract Money CalculateTotalDiscount(Product product);

        public virtual LineItem CreateLineItem(Product product, IEnumerable<ScannedItem> scannedItems, int skipMultiplier)
        {
            return new SpecialLineItem(product.Name, CalculateTotalDiscount(product), GetScannedItemIds(scannedItems, skipMultiplier), Description);
        }
        
        public abstract IEnumerable<int> GetScannedItemIds(IEnumerable<ScannedItem> scannedItems, int skipMultiplier);
    }
}
