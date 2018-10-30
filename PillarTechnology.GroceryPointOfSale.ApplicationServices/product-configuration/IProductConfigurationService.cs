namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface IProductConfigurationService
    {
        ProductDto CreateProduct(CreateProductDto productDto);
        ProductDto UpdateProduct(ProductDto productDto);
    }
}