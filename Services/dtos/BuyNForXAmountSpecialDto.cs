using System;

namespace PointOfSale.ApplicationServices
{
    public class BuyNForXAmountSpecialDto : ISpecialDto
    {
        public string Description { get; set; }
        public int DiscountedItems { get; set; }
        public DateTime EndTime { get; set; }
        public decimal GroupSalePrice { get; set; }
        public bool IsActive { get; set; }
        public int? Limit { get; set; }
        public DateTime StartTime { get; set; }
    }
}
