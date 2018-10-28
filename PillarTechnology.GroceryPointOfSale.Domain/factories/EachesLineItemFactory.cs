namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class EachesLineItemFactory : LineItemFactory
    {
        private readonly IScannable _scannable;

        public EachesLineItemFactory(IScannable scannable)
        {
            _scannable = scannable;
        }

        public override LineItem CreateLineItem()
        {
            return new LineItem(_scannable.Product.Name, _scannable.Product.RetailPrice);
        }
    }
}