namespace PointOfSale.Services
{
    public interface IProductConfigurationService
    {
        ProductDto CreateProduct(UpsertProductArgs args);
        ProductDto UpdateProduct(UpsertProductArgs args);
    }
}
