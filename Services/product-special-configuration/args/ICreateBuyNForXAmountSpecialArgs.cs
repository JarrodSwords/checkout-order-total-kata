namespace PointOfSale.ApplicationServices
{
    public interface ICreateBuyNForXAmountSpecialArgs
    {
        int? DiscountedItems { get; set; }
        decimal? GroupSalePrice { get; set; }
    }
}
