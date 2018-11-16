using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace GroceryPointOfSale.Domain
{
    public partial class BuyNGetMAtXPercentOffSpecial : Special
    {
        public class Factory : SpecialFactory
        {
            protected int DiscountedItems { get; set; }
            protected decimal PercentageOff { get; set; }
            protected int PreDiscountItems { get; set; }

            public Factory(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider) { }

            public Factory Configure(int discountedItems, DateTime endTime, decimal percentageOff, int preDiscountItems, DateTime startTime, int? limit = null)
            {
                Configure(endTime, startTime, limit);
                DiscountedItems = discountedItems;
                PercentageOff = percentageOff;
                PreDiscountItems = preDiscountItems;

                return this;
            }

            public override Special CreateSpecial()
            {
                return new BuyNGetMAtXPercentOffSpecial(new BasicTemporal(EndTime, StartTime), PreDiscountItems, DiscountedItems, PercentageOff, Limit);
            }
        }
    }
}
