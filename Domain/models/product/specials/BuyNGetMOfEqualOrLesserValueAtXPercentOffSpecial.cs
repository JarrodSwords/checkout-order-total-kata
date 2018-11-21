using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PointOfSale.Domain
{
    public partial class BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial : BuyNGetMAtXPercentOffSpecial
    {
        public override string Description => $"buy {PreDiscountItems} get {DiscountedItems} of equal or lesser value at {Multiplier:P} off";

        public BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(
            ITemporal temporal,
            int preDiscountItems,
            int discountedItems,
            decimal percentageOff,
            int? limit = null
        ) : base(temporal, preDiscountItems, discountedItems, percentageOff, limit) { }

        public override Money CalculateTotalDiscount(Product product)
        {
            return 0;
        }

        public override IEnumerable<int> GetScannedItemIds(IEnumerable<ScannedItem> scannedItems, int skipMultiplier)
        {
            return scannedItems.OrderByDescending(x => ((ScannedItemWithMass) x).Mass)
                .Skip(ScannedItemsRequired * skipMultiplier)
                .Take(ScannedItemsRequired)
                .Select(x => x.Id);
        }

        public override LineItem CreateLineItem(Product product, IEnumerable<ScannedItem> scannedItems, int skipMultiplier)
        {
            var itemsInSpecial = scannedItems
                .OrderByDescending(x => ((ScannedItemWithMass) x).Mass)
                .Skip(ScannedItemsRequired * skipMultiplier)
                .Take(ScannedItemsRequired)
                .ToList();

            var discountedItems = itemsInSpecial.Skip(PreDiscountItems).ToList();
            var totalDiscount = Money.USDollar(discountedItems.Sum(x => -(x.SalePrice * Multiplier).Amount));

            return new SpecialLineItem(product.Name, totalDiscount, discountedItems.Select(x => x.Id), Description);
        }
    }
}
