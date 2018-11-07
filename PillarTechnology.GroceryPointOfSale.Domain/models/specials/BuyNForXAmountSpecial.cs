using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class BuyNForXAmountSpecial : Special
    {
        public override string Description => $"buy {DiscountedItems} for {GroupSalePrice}";
        public int DiscountedItems { get; set; }
        public Money GroupSalePrice { get; set; }

        public BuyNForXAmountSpecial() { }

        public BuyNForXAmountSpecial(DateTime startTime, DateTime endTime, int discountedItems, Money groupSalePrice, int? limit = null) : base(startTime, endTime, limit)
        {
            DiscountedItems = discountedItems;
            GroupSalePrice = groupSalePrice;
        }

        public override Money CalculateSalePrice(Product product)
        {
            var totalRetailPrice = product.RetailPrice * DiscountedItems;
            return GroupSalePrice - totalRetailPrice;
        }

        public override int GetScannedItemsRequired()
        {
            return DiscountedItems;
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
