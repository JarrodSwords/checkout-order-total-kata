using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public abstract class ScannedItem : ILineItemFactory
    {
        private int _id;
        private readonly ILineItemFactory _lineItemFactory;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                _lineItemFactory.Id = value;
            }
        }
        public Mass Mass => _lineItemFactory.Mass;
        public Product Product { get; }
        public Money SalePrice => _lineItemFactory.SalePrice;

        public ScannedItem(ILineItemFactory lineItemFactory, Product product)
        {
            _lineItemFactory = lineItemFactory;
            Product = product;
        }

        public MarkdownLineItem CreateMarkdownLineItem() => _lineItemFactory.CreateMarkdownLineItem();
        public RetailLineItem CreateRetailLineItem() => _lineItemFactory.CreateRetailLineItem();
    }
}
