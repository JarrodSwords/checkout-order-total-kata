using System;
using System.Collections.Generic;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public abstract class Special
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ScannedItemsRequired { get { return GetScannedItemsRequired(); } }
        public bool IsActive
        {
            get
            {
                var now = DateTime.Now;
                return now >= StartTime && now <= EndTime;
            }
        }

        public Special() { }

        public Special(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
        
        public bool GetIsBestDiscount(Product product)
        {
            var discount = CalculateSalePrice(product);

            if (discount > 0)
                return false;

            if (product.Markdown == null || !product.Markdown.IsActive)
                return true;

            var markdown = GetScannedItemsRequired() * -product.Markdown.AmountOffRetail;

            return discount < markdown;
        }

        public abstract Money CalculateSalePrice(Product product);
        public abstract string GetLineItemDescription(Product product);
        public abstract IEnumerable<int> GetScannedItemIds(IEnumerable<ScannedItem> scannedItems, int skipMultiplier);
        public abstract int GetScannedItemsRequired();
    }
}
