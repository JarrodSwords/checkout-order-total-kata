using NodaMoney;

namespace PointOfSale.Domain
{
    public class MarkdownableAsEaches : IMarkdownable
    {
        public EachesProduct EachesProduct { get; set; }

        public MarkdownableAsEaches(EachesProduct eachesProduct)
        {
            EachesProduct = eachesProduct;
        }

        public Money GetMarkdownSalePrice() =>
            -EachesProduct.Markdown.AmountOffRetail;
    }
}
