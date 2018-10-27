namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public abstract class ScannableAbstractFactory
    {
        protected readonly Product _product;

        public ScannableAbstractFactory(Product product)
        {
            _product = product;
        }

        public abstract IScannable CreateScannable();
    }
}