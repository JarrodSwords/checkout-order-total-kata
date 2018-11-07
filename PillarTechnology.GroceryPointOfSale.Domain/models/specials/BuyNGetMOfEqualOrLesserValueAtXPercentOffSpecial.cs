using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial : BuyNGetMAtXPercentOffSpecial
    {
        public BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial() { }

        public BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(DateTime startTime, DateTime endTime, int preDiscountItems, int discountedItems, decimal percentageOff, int? limit = null) : base(startTime, endTime, preDiscountItems, discountedItems, percentageOff, limit) { }

        public override Money CalculateSalePrice(Product product)
        {
            return -Money.USDollar(DiscountedItems * product.RetailPrice.Amount * Multiplier);
        }

        public override string GetLineItemDescription(Product product)
        {
            return $"{product.Name} - special - buy {PreDiscountItems} get {DiscountedItems} of equal or lesser value at {Multiplier:P} off";
        }

        public override int GetScannedItemsRequired()
        {
            return PreDiscountItems + DiscountedItems;
        }

        public override IEnumerable<int> GetScannedItemIds(IEnumerable<ScannedItem> scannedItems, int skipMultiplier)
        {
            var scannedItemsRequired = GetScannedItemsRequired();

            return scannedItems.OrderByDescending(x => ((WeightedScannedItem) x).Weight)
                .Skip(scannedItemsRequired * skipMultiplier)
                .Take(scannedItemsRequired)
                .Select(x => x.Id);
        }

        public override LineItem CreateLineItem(Product product, IEnumerable<ScannedItem> scannedItems, int skipMultiplier)
        {
            var scannedItemsRequired = GetScannedItemsRequired();
            var itemsInSpecial = scannedItems
                .OrderByDescending(x => ((WeightedScannedItem) x).Weight)
                .Skip(scannedItemsRequired * skipMultiplier)
                .Take(scannedItemsRequired)
                .ToList();

            var discountedItems = itemsInSpecial.Skip(PreDiscountItems).ToList();
            var totalDiscount = Money.USDollar(discountedItems.Sum(x => CalculateSalePrice(product).Amount * ((WeightedScannedItem) x).Weight));

            return new SpecialLineItem(GetLineItemDescription(product), totalDiscount, discountedItems.Select(x => x.Id));
        }
    }
}
