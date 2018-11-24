namespace PointOfSale.Domain
{
    public partial class BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial : BuyNGetMAtXPercentOffSpecial
    {
        public new class Factory : BuyNGetMAtXPercentOffSpecial.Factory
        {
            public Factory(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider) { }

            public override Special CreateSpecial()
            {
                return new BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(new Temporal(EndTime, StartTime), PreDiscountItems, DiscountedItems, PercentageOff, Limit);
            }
        }
    }
}