namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class WeightedLineItemFactory : LineItemFactory
    {
        private readonly WeightedItem _weightedItem;

        public WeightedLineItemFactory(WeightedItem weightedItem)
        {
            _weightedItem = weightedItem;
        }

        public override LineItem CreateLineItem()
        {
            return new LineItem(_weightedItem.Product.Name, _weightedItem.Product.RetailPrice * _weightedItem.Weight, _weightedItem.Id);
        }
    }
}