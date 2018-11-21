using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public class EachesLineItemFactory : LineItemFactory
    {
        public override Mass Mass =>
            throw new System.NotImplementedException();
        public override Money SalePrice => Product.GetSalePrice();

        public EachesLineItemFactory(Product product) : base(product) { }

        public override MarkdownLineItem CreateMarkdownLineItem() =>
            new MarkdownLineItem(Product.Name, -Product.Markdown.AmountOffRetail, Id);
    }
}
