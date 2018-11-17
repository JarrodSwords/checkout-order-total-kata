using PointOfSale.Domain;

namespace PointOfSale.Test.Domain
{
    public static class SpecialProvider
    {
        public static Special GetBuyNGetMAtXPercentOffSpecial(DateRange dateRange, int preDiscountItems = 2, int discountedItems = 1, decimal percentageOff = 0.5m)
        {
            var dateTimeProvider = DependencyProvider.CreateDateTimeProvider();
            var now = dateTimeProvider.Now;

            return new BuyNGetMAtXPercentOffSpecial.Factory(dateTimeProvider)
                .Configure(discountedItems, dateRange.GetEnd(now), percentageOff, preDiscountItems, dateRange.GetStart(now))
                .CreateSpecial();
        }
    }
}
