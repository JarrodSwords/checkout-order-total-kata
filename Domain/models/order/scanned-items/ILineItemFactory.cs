using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public interface ILineItemFactory
    {
        int Id { get; set; }
        Product Product { get; }
        Mass Mass { get; }
        Money SalePrice { get; }

        MarkdownLineItem CreateMarkdownLineItem();
        RetailLineItem CreateRetailLineItem();
    }

    public abstract class LineItemFactory : ILineItemFactory
    {
        public int Id { get; set; }
        public abstract Mass Mass { get; }
        public abstract Money SalePrice { get; }
        public Product Product { get; }

        public LineItemFactory(Product product)
        {
            Product = product;
        }

        public abstract MarkdownLineItem CreateMarkdownLineItem();

        public RetailLineItem CreateRetailLineItem() =>
            new RetailLineItem(Product.Name, SalePrice, Id);
    }

    public class EachesLineItemFactory : LineItemFactory
    {
        public override Mass Mass =>
            throw new System.NotImplementedException();
        public override Money SalePrice => Product.RetailPrice;

        public EachesLineItemFactory(Product product) : base(product) { }

        public override MarkdownLineItem CreateMarkdownLineItem() =>
            new MarkdownLineItem(Product.Name, -Product.Markdown.AmountOffRetail, Id);
    }

    public class MassLineItemFactory : LineItemFactory
    {
        public override Mass Mass { get; }
        public override Money SalePrice => Product.RetailPricePerUnit * (decimal) Mass.Value;

        public MassLineItemFactory(Mass mass, Product product) : base(product)
        {
            Mass = mass;
        }

        public override MarkdownLineItem CreateMarkdownLineItem() =>
            new MarkdownLineItem(
                Product.Name, -Product.Markdown.AmountOffRetail * (decimal) Mass.Value, Id
            );
    }
}
