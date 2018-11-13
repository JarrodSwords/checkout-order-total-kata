namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface IProductSpecialConfigurationService
    {
        ProductDto CreateSpecial(CreateSpecialArgs args);
    }
}
