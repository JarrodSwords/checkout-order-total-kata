using System;
using System.Collections.Generic;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public abstract class Special
    {
        public Product Product { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Special() { }

        public Special(Product product, DateTime startTime, DateTime endTime)
        {
            Product = product;
            StartTime = startTime;
            EndTime = endTime;
        }

        public abstract IEnumerable<LineItem> CreateLineItems(IEnumerable<ScannedItem> scannedItems);
    }
}