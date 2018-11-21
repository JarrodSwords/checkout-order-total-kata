namespace PointOfSale.Domain
{
    public class ScannedItemAsEaches : ScannedItem
    {
        public ScannedItemAsEaches(Product product) : base(
            new ScannableAsEaches(product)
        ) { }
    }
}
