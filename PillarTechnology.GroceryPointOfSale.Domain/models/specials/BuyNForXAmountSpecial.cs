using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class BuyNForXAmountSpecial : Special
    {
        public int DiscountedItems { get; set; }
        public Money SalePrice { get; set; }

        public BuyNForXAmountSpecial() { }

        public BuyNForXAmountSpecial(DateTime startTime, DateTime endTime, int discountedItems, Money salePrice) : base(startTime, endTime)
        {
            DiscountedItems = discountedItems;
            SalePrice = salePrice;
        }

        public override Money CalculateSalePrice(Product product)
        {
            var totalRetailPrice = product.RetailPrice * DiscountedItems;
            return SalePrice - totalRetailPrice;
        }

        public override string GetLineItemDescription(Product product)
        {
            return $"{product.Name} - special - buy {DiscountedItems} for {SalePrice}";
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
