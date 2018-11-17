namespace PointOfSale.Services
{
    public interface ICreateBuyNForXAmountSpecialArgs
    {
        int? DiscountedItems { get; set; }
        decimal? GroupSalePrice { get; set; }
    }
}
