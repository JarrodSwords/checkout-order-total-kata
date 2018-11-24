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

        protected BuyNGetMAtXPercentOffSpecial(
            ITemporal temporal,
            int preDiscountItems,
            int discountedItems,
            decimal percentageOff,
            int? limit = null
        ) : base(temporal, limit)
        {
            PreDiscountItems = preDiscountItems;
            DiscountedItems = discountedItems;
            PercentageOff = percentageOff;
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
