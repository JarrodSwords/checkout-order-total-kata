namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class WeightedMarkdownLineItemFactory : LineItemFactory
    {
        public WeightedItem WeightedItem { get; private set; }

        public WeightedMarkdownLineItemFactory() { }

        public WeightedMarkdownLineItemFactory(WeightedItem weightedItem)
        {
            WeightedItem = weightedItem;
        }

        public void Configure(WeightedItem weightedItem) => WeightedItem = weightedItem;

        public override LineItem CreateLineItem()
        {
            return new LineItem(WeightedItem.Product.Name + " Markdown", -WeightedItem.Product.Markdown.AmountOffRetail * WeightedItem.Weight);
        }
    }
}