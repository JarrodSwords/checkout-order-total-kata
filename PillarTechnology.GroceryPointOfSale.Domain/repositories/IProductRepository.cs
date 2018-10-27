namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public interface IProductRepository
    {
        Product CreateProduct(Product product);
        Product FindProduct(string productName);
        Product UpdateProduct(Product product);
    }
}