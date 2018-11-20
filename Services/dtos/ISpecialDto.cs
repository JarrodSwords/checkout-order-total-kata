using System;

namespace PointOfSale.Services
{
    public interface ISpecialDto
    {
        string Description { get; set; }
        int DiscountedItems { get; set; }
        DateTime EndTime { get; set; }
        bool IsActive { get; set; }
        int? Limit { get; set; }
        string SpecialType { get; set; }
        DateTime StartTime { get; set; }
    }

    public interface IGroupSalePriceDto
    {
        decimal GroupSalePrice { get; set; }
    }

    public interface IPercentOffPreDiscountItemsDto
    {
        decimal PercentOff { get; set; }
        decimal PreDiscountItems { get; set; }
    }

    public class BuyNForXAmountSpecialDto : ISpecialDto, IGroupSalePriceDto
    {
        public string Description { get; set; }
        public int DiscountedItems { get; set; }
        public DateTime EndTime { get; set; }
        public decimal GroupSalePrice { get; set; }
        public bool IsActive { get; set; }
        public int? Limit { get; set; }
        public string SpecialType { get; set; }
        public DateTime StartTime { get; set; }
    }

    public class BuyNGetMAtXPercentOffSpecialDto : ISpecialDto, IPercentOffPreDiscountItemsDto
    {
        public string Description { get; set; }
        public int DiscountedItems { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public int? Limit { get; set; }
        public decimal PercentOff { get; set; }
        public decimal PreDiscountItems { get; set; }
        public string SpecialType { get; set; }
        public DateTime StartTime { get; set; }
    }

    public class BuyNGetMOfEqualOrLesserValueAtXPercentOffDto : ISpecialDto, IPercentOffPreDiscountItemsDto
    {
        public string Description { get; set; }
        public int DiscountedItems { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public int? Limit { get; set; }
        public decimal PercentOff { get; set; }
        public decimal PreDiscountItems { get; set; }
        public string SpecialType { get; set; }
        public DateTime StartTime { get; set; }
    }
}
