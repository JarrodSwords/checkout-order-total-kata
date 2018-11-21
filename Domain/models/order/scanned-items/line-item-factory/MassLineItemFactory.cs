using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public class MassLineItemFactory : LineItemFactory
    {
        public override Mass Mass { get; }
        public override Money SalePrice => Product.GetSalePrice(Mass);

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
