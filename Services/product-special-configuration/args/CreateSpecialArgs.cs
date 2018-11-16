using System;

namespace PointOfSale.ApplicationServices
{
    public class CreateSpecialArgs : ICreateBuyNForXAmountSpecialArgs, ICreateBuyNGetMAtXPercentOffSpecialArgs
    {
        public int? DiscountedItems { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? GroupSalePrice { get; set; }
        public int? Limit { get; set; }
        public decimal? PercentageOff { get; set; }
        public int? PreDiscountItems { get; set; }
        public string ProductName { get; set; }
        public string SpecialType { get; set; }
        public DateTime? StartTime { get; set; }
    }
}
