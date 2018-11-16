namespace PointOfSale.ApplicationServices
{
    public interface IProductSpecialConfigurationService
    {
        ProductDto CreateSpecial(CreateSpecialArgs args);
    }
}
