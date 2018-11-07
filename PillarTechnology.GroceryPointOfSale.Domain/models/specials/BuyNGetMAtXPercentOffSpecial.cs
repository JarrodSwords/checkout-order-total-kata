using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class BuyNGetMAtXPercentOffSpecial : Special
    {
        public override string Description => $"buy {PreDiscountItems} get {DiscountedItems} at {Multiplier:P0} off";
        public int DiscountedItems { get; }
        public decimal Multiplier { get; }
        public int PreDiscountItems { get; }
        public override int ScannedItemsRequired => PreDiscountItems + DiscountedItems;

        public BuyNGetMAtXPercentOffSpecial() { }

        public BuyNGetMAtXPercentOffSpecial(DateTime startTime, DateTime endTime, int preDiscountItems, int discountedItems, decimal percentageOff, int? limit = null) : base(startTime, endTime, limit)
        {
            PreDiscountItems = preDiscountItems;
            DiscountedItems = discountedItems;
            Multiplier = percentageOff / 100;
        }

        public override Money CalculateTotalDiscount(Product product)
        {
            return -Money.USDollar(DiscountedItems * product.RetailPrice.Amount * Multiplier);
        }

        public override IEnumerable<int> GetScannedItemIds(IEnumerable<ScannedItem> scannedItems, int skipMultiplier)
        {
            return scannedItems
                .Skip(ScannedItemsRequired * skipMultiplier)
                .Take(ScannedItemsRequired)
                .Select(x => x.Id);
        }
    }
}
