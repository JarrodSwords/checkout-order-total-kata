using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public interface IScannable
    {
        Product Product { get; }
        Money MarkdownDiscount { get; }
        Mass Mass { get; }
        Money RetailPrice { get; }
    }

    public class ScannableAsEaches : IScannable
    {
        public Money MarkdownDiscount =>
            -Product.Markdown.AmountOffRetail;

        public Mass Mass =>
            throw new System.NotImplementedException();

        public Product Product { get; }

        public Money RetailPrice =>
            Product.RetailPrice;

        public ScannableAsEaches(Product product)
        {
            Product = product;
        }
    }

    public class ScannableWithMass : IScannable
    {
        public Money MarkdownDiscount =>
            Product.Markdown.AmountOffRetail * (decimal) Mass.Value;

        public Mass Mass { get; }

        public Money RetailPrice =>
            Product.RetailPricePerUnit * (decimal) Mass.Value;

        public Product Product { get; }

        public ScannableWithMass(Mass mass, Product product)
        {
            Mass = mass;
            Product = product;
        }
    }
}
