namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface IProductSpecialConfigurationService
    {
        ProductDto CreateBuyNForXAmountSpecial(CreateBuyNForXAmountSpecialArgs args);
        ProductDto CreateBuyNGetMAtXPercentOffSpecial(CreateBuyNGetMAtXPercentOffSpecialArgs args);
    }
}
