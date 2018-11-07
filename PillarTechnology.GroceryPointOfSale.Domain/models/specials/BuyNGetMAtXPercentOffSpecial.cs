using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class BuyNGetMAtXPercentOffSpecial : Special
    {
        public int PreDiscountItems { get; set; }
        public int DiscountedItems { get; set; }
        public decimal Multiplier { get; set; }

        public override string Description => $"buy {PreDiscountItems} get {DiscountedItems} at {Multiplier:P0} off";

        public BuyNGetMAtXPercentOffSpecial() { }

        public BuyNGetMAtXPercentOffSpecial(DateTime startTime, DateTime endTime, int preDiscountItems, int discountedItems, decimal percentageOff, int? limit = null) : base(startTime, endTime, limit)
        {
            PreDiscountItems = preDiscountItems;
            DiscountedItems = discountedItems;
            Multiplier = percentageOff / 100;
        }

        public override Money CalculateSalePrice(Product product)
        {
            return -Money.USDollar(DiscountedItems * product.RetailPrice.Amount * Multiplier);
        }

        public override int GetScannedItemsRequired()
        {
            return PreDiscountItems + DiscountedItems;
        }

        public override IEnumerable<int> GetScannedItemIds(IEnumerable<ScannedItem> scannedItems, int skipMultiplier)
        {
            var scannedItemsRequired = GetScannedItemsRequired();

            return scannedItems
                .Skip(scannedItemsRequired * skipMultiplier)
                .Take(scannedItemsRequired)
                .Select(x => x.Id);
        }
    }
}
