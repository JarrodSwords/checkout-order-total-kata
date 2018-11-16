namespace PointOfSale.ApplicationServices
{
    public interface IProductService
    {
        ProductDto FindProduct(string productName);
    }
}