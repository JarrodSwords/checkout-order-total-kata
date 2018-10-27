namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class ItemFactory : ScannableAbstractFactory
    {
        public ItemFactory(Product product) : base(product) { }

        public override IScannable CreateScannable()
        {
            return new Item(_product);
        }
    }
}