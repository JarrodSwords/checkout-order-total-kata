namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface IProductMarkdownConfigurationService
    {
        ProductDto UpsertProductMarkdown(UpsertProductMarkdownArgs args);
    }
}