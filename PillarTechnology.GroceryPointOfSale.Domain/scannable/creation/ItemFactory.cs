namespace PillarTechnology.GroceryPointOfSale.Domain
{
    /// <remarks>
    /// Concrete factory
    /// </summary>
    public class ItemFactory : ScannableFactory
    {
        public ItemFactory() { }

        public ItemFactory(Product product) : base(product) { }

        public override ScannedItem CreateScannable()
        {
            return new ScannedItem(Product);
        }

        public void Configure(Product product)
        {
            Product = product;
        }
    }
}