using PointOfSale.Domain;

namespace PointOfSale.Services
{
    public interface IProductDto
    {
        MarkdownDto Markdown { get; set; }
        string Name { get; set; }
        string SellByType { get; set; }
        ISpecialDto Special { get; set; }
    }

    public class ProductDto : IProductDto
    {
        public MarkdownDto Markdown { get; set; }
        public string Name { get; set; }
        public string SellByType { get; set; }
        public ISpecialDto Special { get; set; }
    }

    public class EachesProductDto : ProductDto
    {
        public decimal RetailPrice { get; set; }
    }

    public class MassDto
    {
        double Amount { get; set; }
        string Unit { get; set; }
    }

    public class MassProductDto : ProductDto
    {
        public MassDto Mass { get; set; }
    }
}
