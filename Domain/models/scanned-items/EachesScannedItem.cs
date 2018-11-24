namespace PointOfSale.Domain
{
    public class EachesScannedItem : ScannedItem
    {
        public EachesScannedItem(EachesProduct product) : base(
            new MarkdownableAsEaches(product),
            product,
            new SellableAsEaches(product)
        ) { }
    }
}
