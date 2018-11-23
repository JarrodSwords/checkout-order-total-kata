using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    /// <summary>
    /// A good available for sale
    /// </summary>
    public abstract class Product : IMarkdownable, ISellable
    {
        private readonly IMarkdownable _markdownable;
        private readonly ISellable _sellable;

        public bool HasActiveMarkdown => Markdown != null && Markdown.IsActive;
        public bool HasActiveSpecial => Special != null && Special.IsActive;
        public Markdown Markdown
        {
            get => _markdownable.Markdown;
            set => _markdownable.Markdown = value;
        }
        public Mass Mass
        {
            get => _sellable.Mass;
            set => _sellable.Mass = value;
        }
        public string Name { get; }
        public Money RetailPrice
        {
            get => _sellable.RetailPrice;
            set => _sellable.RetailPrice = value;
        }
        public Special Special { get; set; }

        public Product(string name, IMarkdownable markdownable, ISellable sellable)
        {
            Name = name;
            _markdownable = markdownable;
            _sellable = sellable;
        }

        public Money GetMarkdownSalePrice() =>
            _markdownable.GetMarkdownSalePrice();

        public Money GetMarkdownSalePrice(Mass mass) =>
            _markdownable.GetMarkdownSalePrice(mass);

        public Money GetSalePrice() =>
            _sellable.GetSalePrice();

        public Money GetSalePrice(Mass mass) =>
            _sellable.GetSalePrice(mass);
    }
}
