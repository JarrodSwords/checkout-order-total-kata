using NodaMoney;

namespace PointOfSale.Domain
{
    public class WeightedScannedItem : ScannedItem
    {
        public override Money MarkdownDiscount => Product.Markdown.AmountOffRetail * Weight;
        public decimal Weight { get; }
        public override Money RetailPrice => Product.RetailPricePerUnit * Weight;

        public WeightedScannedItem(Product product, decimal weight) : base(product)
        {
            Weight = weight;
        }
    }
}
