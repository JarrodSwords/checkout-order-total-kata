namespace PillarTechnology.GroceryPointOfSale.Domain
{
    /// <remarks>
    /// Abstract factory
    /// </remarks>
    public abstract class ScannableFactory
    {
        public Product Product { get; set; }

        public ScannableFactory() { }

        public ScannableFactory(Product product)
        {
            Product = product;
        }

        public abstract ScannedItem CreateScannable();
    }
}