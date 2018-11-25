using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PointOfSale.Domain
{
    public partial class BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial : BuyNGetMAtXPercentOffSpecial
    {
        public override string Description => $"buy {PreDiscountItems} get {DiscountedItems} of equal or lesser value at {Multiplier:P} off";

        public BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(
            int discountedItems,
            DateTime endTime,
            decimal percentageOff,
            int preDiscountItems,
            DateTime startTime,
            int? limit = null
        ) : base(discountedItems, endTime, percentageOff, preDiscountItems, startTime, limit) { }

        public override Money CalculateTotalDiscount(IEnumerable<ScannedItem> scannedItems)
        {
            Money totalDiscount = 0;

            foreach (var scannedItem in scannedItems.Skip(PreDiscountItems))
            {
                var scannedMass = ((MassScannedItem) scannedItem).Mass;
                var soldByMass = ((MassProduct) scannedItem.Product).Mass;

                var discountRetailPrice = scannedItem.Product.RetailPrice * ((100 - PercentageOff) / 100);
                var massMultiplier = (decimal) (scannedMass / soldByMass);
                var retailPrice = scannedItem.GetSalePrice();
                var discountPrice = discountRetailPrice * massMultiplier;

                totalDiscount += discountPrice - retailPrice;
            }

            return totalDiscount;
        }
    }
}
