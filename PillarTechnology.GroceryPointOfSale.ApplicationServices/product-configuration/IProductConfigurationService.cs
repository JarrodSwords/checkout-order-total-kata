namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface IProductConfigurationService
    {
        ProductDto CreateProduct(UpsertProductDto dto);
        ProductDto UpdateProduct(UpsertProductDto dto);
        ProductDto UpsertProductMarkdown(UpsertProductMarkdownDto dto);
    }
}