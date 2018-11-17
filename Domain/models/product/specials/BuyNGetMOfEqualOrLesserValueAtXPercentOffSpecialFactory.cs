using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PointOfSale.Domain
{
    public partial class BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial : BuyNGetMAtXPercentOffSpecial
    {
        public new class Factory : BuyNGetMAtXPercentOffSpecial.Factory
        {
            public Factory(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider) { }

            public override Special CreateSpecial()
            {
                return new BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(new BasicTemporal(EndTime, StartTime), PreDiscountItems, DiscountedItems, PercentageOff, Limit);
            }
        }
    }
}
