namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class WeightedLineItemFactory : LineItemFactory
    {
        public WeightedItem WeightedItem { get; private set; }

        public WeightedLineItemFactory() { }

        public WeightedLineItemFactory(WeightedItem weightedItem) => WeightedItem = weightedItem;

        public void Configure(WeightedItem weightedItem) => WeightedItem = weightedItem;

        public override LineItem CreateLineItem()
        {
            return new LineItem(WeightedItem.Product.Name, WeightedItem.Product.RetailPrice * WeightedItem.Weight, WeightedItem.Id);
        }
    }
}