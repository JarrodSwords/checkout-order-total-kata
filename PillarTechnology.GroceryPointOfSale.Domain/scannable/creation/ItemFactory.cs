namespace PillarTechnology.GroceryPointOfSale.Domain
{
    /// <remarks>
    /// Concrete factory
    /// </summary>
    public class ItemFactory : ScannableFactory
    {
        public ItemFactory(Product product) : base(product) { }

        public override IScannable CreateScannable()
        {
            return new Item(Product);
        }
    }
}