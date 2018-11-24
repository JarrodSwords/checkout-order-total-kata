using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public abstract class ScannedItem : IMarkdownable, IScannedItem, ISellable
    {
        protected readonly IMarkdownable _markdownable;
        protected readonly ISellable _sellable;

        public int Id { get; set; }
        public Product Product { get; }

        public ScannedItem(IMarkdownable markdownable, Product product, ISellable sellable)
        {
            _markdownable = markdownable;
            Product = product;
            _sellable = sellable;
        }

        public MarkdownLineItem CreateMarkdownLineItem() =>
            new MarkdownLineItem(
                Product.Name,
                GetMarkdownSalePrice(),
                Id
            );

        public RetailLineItem CreateRetailLineItem() =>
            new RetailLineItem(
                Product.Name,
                GetSalePrice(),
                Id
            );

        public Money GetMarkdownSalePrice() =>
            _markdownable.GetMarkdownSalePrice();

        public Money GetSalePrice() =>
            _sellable.GetSalePrice();
    }
}
