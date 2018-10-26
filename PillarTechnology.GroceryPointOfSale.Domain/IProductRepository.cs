namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        Product FindProduct(string productName);
    }
}