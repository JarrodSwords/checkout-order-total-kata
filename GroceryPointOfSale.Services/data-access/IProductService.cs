namespace GroceryPointOfSale.ApplicationServices
{
    public interface IProductService
    {
        ProductDto FindProduct(string productName);
    }
}