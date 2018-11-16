using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace GroceryPointOfSale.Domain
{
    public partial class BuyNForXAmountSpecial : Special
    {
        public class Factory : SpecialFactory
        {
            private int DiscountedItems { get; set; }
            private Money GroupSalePrice { get; set; }

            public Factory(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider) { }

            public Factory Configure(int discountedItems, DateTime endTime, Money groupSalePrice, DateTime startTime, int? limit = null)
            {
                Configure(endTime, startTime, limit);
                DiscountedItems = discountedItems;
                GroupSalePrice = groupSalePrice;

                return this;
            }

            public override Special CreateSpecial()
            {
                return new BuyNForXAmountSpecial(new BasicTemporal(EndTime, StartTime), DiscountedItems, GroupSalePrice, Limit);
            }
        }
    }
}
