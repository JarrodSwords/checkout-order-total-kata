namespace PillarTechnology.GroceryPointOfSale.Domain
{
    /// <remarks>
    /// Concrete factory
    /// </summary>
    public class WeightedItemFactory : ScannableFactory
    {
        private readonly decimal _weight;

        public WeightedItemFactory(Product product, decimal weight) : base(product)
        {
            _weight = weight;
        }

        public override IScannable CreateScannable()
        {
            return new WeightedItem(Product, _weight);
        }
    }
}