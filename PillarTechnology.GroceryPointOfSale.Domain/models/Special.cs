using System;
using System.Collections.Generic;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public abstract class Special
    {
        public abstract string Description { get; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public abstract int ScannedItemsRequired { get; }
        public int? Limit { get; set; }
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

        public bool GetIsBestDiscount(Product product)
        {
            var discount = CalculateSalePrice(product);

            if (discount > 0)
                return false;

            if (!product.HasActiveMarkdown)
                return true;

            var markdown = ScannedItemsRequired * -product.Markdown.AmountOffRetail;

            return discount < markdown;
        }

        public abstract Money CalculateSalePrice(Product product);
        public abstract IEnumerable<int> GetScannedItemIds(IEnumerable<ScannedItem> scannedItems, int skipMultiplier);
        public virtual LineItem CreateLineItem(Product product, IEnumerable<ScannedItem> scannedItems, int skipMultiplier)
        {
            return new SpecialLineItem(product.Name, CalculateSalePrice(product), GetScannedItemIds(scannedItems, skipMultiplier), Description);
        }
    }
}
