namespace PillarTechnology.GroceryPointOfSale.Domain
{
    /// <remarks>
    /// Abstract factory
    /// </remarks>
    public abstract class ScannableFactory
    {
        protected readonly Product _product;

        public ScannableFactory(Product product)
        {
            _product = product;
        }

        public abstract IScannable CreateScannable();
    }
}