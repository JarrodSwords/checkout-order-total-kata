namespace GroceryPointOfSale.Domain
{
    public interface IProductRepository
    {
        Product CreateProduct(Product product);
        bool Exists(string productName);
        Product FindProduct(string productName);
        Product UpdateProduct(Product product);
    }
}