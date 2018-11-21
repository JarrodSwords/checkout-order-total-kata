using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
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

        public RetailLineItem CreateRetailLineItem() => new RetailLineItem(Product.Name, SalePrice, Id);
    }
}
