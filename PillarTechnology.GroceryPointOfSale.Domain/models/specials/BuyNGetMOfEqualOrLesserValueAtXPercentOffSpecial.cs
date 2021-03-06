using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial : BuyNGetMAtXPercentOffSpecial
    {
        public override string Description => $"buy {PreDiscountItems} get {DiscountedItems} of equal or lesser value at {Multiplier:P} off";

        public BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial() { }

        public BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(DateTime startTime, DateTime endTime, int preDiscountItems, int discountedItems, decimal percentageOff, int? limit = null) : base(startTime, endTime, preDiscountItems, discountedItems, percentageOff, limit) { }

        public override IEnumerable<int> GetScannedItemIds(IEnumerable<ScannedItem> scannedItems, int skipMultiplier)
        {
            return scannedItems.OrderByDescending(x => ((WeightedScannedItem) x).Weight)
                .Skip(ScannedItemsRequired * skipMultiplier)
                .Take(ScannedItemsRequired)
                .Select(x => x.Id);
        }

        public override LineItem CreateLineItem(Product product, IEnumerable<ScannedItem> scannedItems, int skipMultiplier)
        {
            var itemsInSpecial = scannedItems
                .OrderByDescending(x => ((WeightedScannedItem) x).Weight)
                .Skip(ScannedItemsRequired * skipMultiplier)
                .Take(ScannedItemsRequired)
                .ToList();

            var discountedItems = itemsInSpecial.Skip(PreDiscountItems).ToList();
            var totalDiscount = Money.USDollar(discountedItems.Sum(x => -(x.RetailPrice * Multiplier).Amount));

            return new SpecialLineItem(product.Name, totalDiscount, discountedItems.Select(x => x.Id), Description);
        }
    }
}
