using System;

namespace PointOfSale.Services
{
    public interface IDiscountedItemsArgs
    {
        int? DiscountedItems { get; set; }
    }

    public interface IGroupSalePriceArgs
    {
        decimal? GroupSalePrice { get; set; }
    }

    public interface ILimitArgs
    {
        int? Limit { get; set; }
    }

    public interface IPercentageOffArgs
    {
        decimal? PercentageOff { get; set; }
    }

    public interface IPreDiscountItemsArgs
    {
        int? PreDiscountItems { get; set; }
    }

    public interface IProductNameArgs
    {
        string ProductName { get; set; }
    }

    public interface ISellByTypeArgs
    {
        string SellByType { get; set; }
    }

    public interface ISpecialTypeArgs
    {
        string SpecialType { get; set; }
    }
    
    public interface ITemporalArgs
    {
        DateTime? EndTime { get; set; }
        DateTime? StartTime { get; set; }
    }
}
