using PointOfSale.Domain;

namespace PointOfSale.Services
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
