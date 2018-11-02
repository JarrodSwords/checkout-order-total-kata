namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface IProductConfigurationService
    {
        ProductDto CreateProduct(UpsertProductArgs args);
        ProductDto UpdateProduct(UpsertProductArgs args);
        ProductDto UpsertProductMarkdown(UpsertProductMarkdownArgs args);
    }
}