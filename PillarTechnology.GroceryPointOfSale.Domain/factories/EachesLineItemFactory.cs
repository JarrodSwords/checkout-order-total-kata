namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class EachesLineItemFactory : LineItemFactory
    {
        public IScannable Scannable { get; private set; }

        public EachesLineItemFactory() { }

        public EachesLineItemFactory(IScannable scannable)
        {
            Scannable = scannable;
        }

        public void Configure(IScannable scannable) => Scannable = scannable;

        public override LineItem CreateLineItem()
        {
            return new LineItem(Scannable.Product.Name, Scannable.Product.RetailPrice, Scannable.Id);
        }
    }
}