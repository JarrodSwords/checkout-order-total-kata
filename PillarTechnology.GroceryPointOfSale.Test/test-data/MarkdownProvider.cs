using System;
using NodaMoney;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public static class MarkdownProvider
    {
        public static Markdown GetMarkdown(Money amountOffRetail, DateRange dateRange)
        {
            var now = DateTime.Now;
            return new Markdown(amountOffRetail, dateRange.GetStart(now), dateRange.GetEnd(now));
        }
    }

    public static class SpecialProvider
    {
        public static Special GetBuyNGetMAtXPercentOffSpecial(DateRange dateRange, int preDiscountItems = 2, int discountedItems = 1, decimal percentageOff = 0.5m)
        {
            var now = DateTime.Now;
            return new BuyNGetMAtXPercentOffSpecial(dateRange.GetStart(now), dateRange.GetEnd(now), preDiscountItems, discountedItems, percentageOff);
        }
    }
}
