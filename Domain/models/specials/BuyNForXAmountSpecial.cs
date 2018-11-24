using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PointOfSale.Domain
{
    public partial class BuyNForXAmountSpecial : Special
    {
        public override string Description => $"buy {DiscountedItems} for {GroupSalePrice}";
        public int DiscountedItems { get; }
        public Money GroupSalePrice { get; }
        public override int ScannedItemsRequired => DiscountedItems;

        private BuyNForXAmountSpecial(
            ITemporal temporal,
            int discountedItems,
            Money groupSalePrice,
            int? limit = null
        ) : base(temporal, limit)
        {
            DiscountedItems = discountedItems;
            GroupSalePrice = groupSalePrice;
        }

        public override Money CalculateTotalDiscount(IEnumerable<ScannedItem> scannedItems)
        {
            var product = scannedItems.First().Product;
            var totalRetailPrice = product.RetailPrice * DiscountedItems;
            return GroupSalePrice - totalRetailPrice;
        }
    }
}
