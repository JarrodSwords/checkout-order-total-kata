namespace PointOfSale.Services
{
    public interface ICreateBuyNGetMAtXPercentOffSpecialArgs
    {
        int? DiscountedItems { get; set; }
        decimal? PercentageOff { get; set; }
        int? PreDiscountItems { get; set; }
    }
}
