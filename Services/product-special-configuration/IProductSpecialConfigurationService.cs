namespace PointOfSale.Services
{
    public interface IProductSpecialConfigurationService
    {
        ProductDto CreateSpecial(CreateSpecialArgs args);
    }
}
