namespace PointOfSale.ApplicationServices
{
    public interface IProductMarkdownConfigurationService
    {
        ProductDto UpsertProductMarkdown(UpsertProductMarkdownArgs args);
    }
}