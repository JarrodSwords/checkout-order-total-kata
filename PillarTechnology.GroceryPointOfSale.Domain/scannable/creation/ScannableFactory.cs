namespace PillarTechnology.GroceryPointOfSale.Domain
{
    /// <remarks>
    /// Abstract factory
    /// </remarks>
    public abstract class ScannableFactory
    {
        public Product Product { get; set; }

        public ScannableFactory(Product product)
        {
            Product = product;
        }

        public abstract IScannable CreateScannable();
    }
}