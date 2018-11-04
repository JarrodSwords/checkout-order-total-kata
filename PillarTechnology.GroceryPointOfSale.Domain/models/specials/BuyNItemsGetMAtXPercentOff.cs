using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class BuyNItemsGetMAtXPercentOff : Special
    {
        public int PreDiscountItems { get; set; }
        public int DiscountedItems { get; set; }
        public decimal Multiplier { get; set; }
        public string Description { get { return $"{Product.Name} - special buy {PreDiscountItems} get {DiscountedItems} at {Multiplier:P}"; } }

        public BuyNItemsGetMAtXPercentOff() { }

        public BuyNItemsGetMAtXPercentOff(Product product, DateTime startTime, DateTime endTime, int preDiscountItems, int discountedItems, decimal multiplier) : base(product, startTime, endTime)
        {
            PreDiscountItems = preDiscountItems;
            DiscountedItems = discountedItems;
            Multiplier = multiplier;
        }

        public override IEnumerable<LineItem> CreateLineItems(IEnumerable<ScannedItem> scannedItems)
        {
            var specialsApplied = scannedItems.Count() / (PreDiscountItems + DiscountedItems);

            for (var i = 0; i < specialsApplied; i++)
                yield return new SpecialLineItem(Description, -Money.USDollar(DiscountedItems * Product.RetailPrice.Amount * Multiplier));
        }
    }
}