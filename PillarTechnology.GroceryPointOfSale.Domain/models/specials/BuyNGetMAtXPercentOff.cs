using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class BuyNGetMAtXPercentOffSpecial : Special
    {
        public int PreDiscountItems { get; set; }
        public int DiscountedItems { get; set; }
        public decimal Multiplier { get; set; }

        public BuyNGetMAtXPercentOffSpecial() { }

        public BuyNGetMAtXPercentOffSpecial(DateTime startTime, DateTime endTime, int preDiscountItems, int discountedItems, decimal percentageOff) : base(startTime, endTime)
        {
            PreDiscountItems = preDiscountItems;
            DiscountedItems = discountedItems;
            Multiplier = percentageOff / 100;
        }

        public override Money CalculateSalePrice(Product product)
        {
            return -Money.USDollar(DiscountedItems * product.RetailPrice.Amount * Multiplier);
        }

        public override string GetLineItemDescription(Product product)
        {
            return $"{product.Name} - special - buy {PreDiscountItems} get {DiscountedItems} at {Multiplier:P} off";
        }

        public override int GetScannedItemsRequired()
        {
            return PreDiscountItems + DiscountedItems;
        }
    }
}
