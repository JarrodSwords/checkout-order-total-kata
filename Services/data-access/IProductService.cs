namespace PointOfSale.Services
{
    public interface IProductService
    {
        ProductDto FindProduct(string productName);
    }
}