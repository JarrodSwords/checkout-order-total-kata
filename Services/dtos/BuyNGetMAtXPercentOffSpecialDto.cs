using System;

namespace PointOfSale.Services
{
    public class BuyNGetMAtXPercentOffSpecialDto : ISpecialDto
    {
        public int DiscountedItems { get; set; }
        public decimal PercentageOff { get; set; }
        public int PreDiscountItems { get; set; }
        public string Description { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public int? Limit { get; set; }
        public DateTime StartTime { get; set; }
    }
}
