using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
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

        public override Money CalculateTotalDiscount(Product product)
        {
            var totalRetailPrice = product.RetailPrice * DiscountedItems;
            return GroupSalePrice - totalRetailPrice;
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
