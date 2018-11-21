namespace PointOfSale.Domain
{
    public class ScannedItemAsEaches : ScannedItem
    {
        public ScannedItemAsEaches(Product product) : base(
            new EachesLineItemFactory(product),
            product
        ) { }
    }
}
