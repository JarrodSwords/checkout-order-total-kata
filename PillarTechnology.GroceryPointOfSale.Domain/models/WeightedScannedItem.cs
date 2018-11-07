using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class WeightedScannedItem : ScannedItem
    {
        public override Money MarkdownDiscount { get { return Product.Markdown.AmountOffRetail * Weight; } }
        public decimal Weight { get; }
        public override Money RetailPrice { get { return Product.RetailPrice * Weight; } }

        public WeightedScannedItem(Product product, decimal weight) : base(product)
        {
            Weight = weight;
        }
    }
}
