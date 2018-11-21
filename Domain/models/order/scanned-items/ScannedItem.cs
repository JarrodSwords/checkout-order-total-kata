using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public abstract class ScannedItem : IScannable
    {
        private readonly IScannable _scannable;

        public int Id { get; set; }

        public Money MarkdownDiscount =>
            _scannable.MarkdownDiscount;

        public Mass Mass =>
            _scannable.Mass;

        public Product Product =>
            _scannable.Product;

        public Money RetailPrice =>
            _scannable.RetailPrice;

        public ScannedItem(IScannable scannable)
        {
            _scannable = scannable;
        }

        public virtual LineItem CreateMarkdownLineItem()
        {
            return new MarkdownLineItem(Product.Name, MarkdownDiscount, Id);
        }

        public LineItem CreateRetailLineItem()
        {
            return new RetailLineItem(Product.Name, RetailPrice, Id);
        }
    }
}
