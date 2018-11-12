using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public partial class BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial : BuyNGetMAtXPercentOffSpecial
    {
        public class Factory : BuyNGetMAtXPercentOffSpecial.Factory
        {
            public Factory(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider) { }

            public override Special CreateSpecial()
            {
                return new BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(new BasicTemporal(EndTime, StartTime), PreDiscountItems, DiscountedItems, PercentageOff, Limit);
            }
        }
    }
}
