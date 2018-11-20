namespace PointOfSale.Services
{
    public interface ICreateBuyNAtXAmountSpecialArgs:
        IDiscountedItemsArgs,
        IGroupSalePriceArgs,
        ILimitArgs,
        IProductNameArgs,
        ISpecialTypeArgs,
        ITemporalArgs { }

    public interface ICreateBuyNGetMAtXPercentOffSpecialArgs:
        IDiscountedItemsArgs,
        ILimitArgs,
        IPercentageOffArgs,
        IPreDiscountItemsArgs,
        IProductNameArgs,
        ISpecialTypeArgs,
        ITemporalArgs { }

    public interface ICreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgs:
        IDiscountedItemsArgs,
        ILimitArgs,
        IPercentageOffArgs,
        IPreDiscountItemsArgs,
        IProductNameArgs,
        ISpecialTypeArgs,
        ITemporalArgs { }
}
