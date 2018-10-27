namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class WeightedItemFactory : ScannableAbstractFactory
    {
        private readonly decimal _weight;

        public WeightedItemFactory(Product product, decimal weight) : base(product)
        {
            _weight = weight;
        }

        public override IScannable CreateScannable()
        {
            return new WeightedItem(_product, _weight);
        }
    }
}