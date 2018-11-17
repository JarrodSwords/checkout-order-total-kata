namespace PointOfSale.Services
{
    public interface IProductMarkdownConfigurationService
    {
        ProductDto UpsertProductMarkdown(UpsertProductMarkdownArgs args);
    }
}