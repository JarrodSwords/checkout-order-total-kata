using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal RetailPrice { get; set; }
        public string SellByType { get; set; }
        public MarkdownDto Markdown { get; set; }
        public ISpecialDto Special { get; set; }
    }
}
