namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class WeightedScannedItem : ScannedItem
    {
        private decimal _weight;

        public decimal Weight { get { return _weight; } }

        public WeightedScannedItem(Product product, decimal weight) : base(product)
        {
            _weight = weight;
        }
        
        public override LineItem CreateLineItem()
        {
            return new LineItem(Product.Name, Product.RetailPrice * Weight, Id);
        }

        public override LineItem CreateMarkdownLineItem()
        {
            return new LineItem($"{Product.Name} markdown", -Product.Markdown.AmountOffRetail * Weight);
        }
    }
}