using GroceryPointOfSale.Domain;

namespace GroceryPointOfSale.Test
{
    public static class SpecialProvider
    {
        public static Special GetBuyNGetMAtXPercentOffSpecial(DateRange dateRange, int preDiscountItems = 2, int discountedItems = 1, decimal percentageOff = 0.5m)
        {
            var dateTimeProvider = new BasicDateTimeProvider();
            var now = dateTimeProvider.Now;

            return new BuyNGetMAtXPercentOffSpecial.Factory(dateTimeProvider)
                .Configure(discountedItems, dateRange.GetEnd(now), percentageOff, preDiscountItems, dateRange.GetStart(now))
                .CreateSpecial();
        }
    }
}
