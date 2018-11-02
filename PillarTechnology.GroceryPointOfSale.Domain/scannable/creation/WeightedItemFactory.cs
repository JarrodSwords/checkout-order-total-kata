namespace PillarTechnology.GroceryPointOfSale.Domain
{
    /// <remarks>
    /// Concrete factory
    /// </summary>
    public class WeightedItemFactory : ScannableFactory
    {
        public decimal Weight { get; private set; }

        public WeightedItemFactory() { }

        public WeightedItemFactory(Product product, decimal weight) : base(product)
        {
            Weight = weight;
        }

        public override ScannedItem CreateScannable()
        {
            return new ScannedWeightedItem(Product, Weight);
        }

        public void Configure(Product product, decimal weight)
        {
            Product = product;
            Weight = weight;
        }
    }
}