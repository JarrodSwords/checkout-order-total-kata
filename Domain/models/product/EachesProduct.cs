namespace PointOfSale.Domain
{
    public class EachesProduct : Product
    {
        public EachesProduct(string name, decimal retailPrice) : base(
            name,
            new MarkdownableAsEaches(),
            new SellableAsEaches(retailPrice)
        ) { }
    }
}
