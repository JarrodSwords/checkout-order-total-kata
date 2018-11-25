using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PointOfSale.Domain
{
    public partial class BuyNGetMAtXPercentOffSpecial : Special
    {
        public override string Description => $"buy {PreDiscountItems} get {DiscountedItems} at {Multiplier:P0} off";
        public int DiscountedItems { get; }
        public decimal PercentageOff { get; }
        public decimal Multiplier => PercentageOff / 100;
        public int PreDiscountItems { get; }
        public override int ScannedItemsRequired => PreDiscountItems + DiscountedItems;

        public BuyNGetMAtXPercentOffSpecial(
            int discountedItems,
            DateTime endTime,
            decimal percentageOff,
            int preDiscountItems,
            DateTime startTime,
            int? limit = null
        ) : base(endTime, startTime, limit)
        {
            PreDiscountItems = preDiscountItems;
            DiscountedItems = discountedItems;
            PercentageOff = percentageOff;
        }

        public override Money CalculateTotalDiscount(IEnumerable<ScannedItem> scannedItems)
        {
            var product = scannedItems.First().Product;
            return -Money.USDollar(DiscountedItems * product.RetailPrice.Amount * Multiplier);
        }
    }
}
