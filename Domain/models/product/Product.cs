using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    /// <summary>
    /// A good available for sale
    /// </summary>
    public abstract class Product : ISellable
    {
        private readonly ISellable _sellable;

        public bool HasActiveMarkdown => Markdown != null && Markdown.IsActive;
        public bool HasActiveSpecial => Special != null && Special.IsActive;
        public Markdown Markdown { get; set; }
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

        public Product(string name, ISellable sellable)
        {
            Name = name;
            _sellable = sellable;
        }

        public Money GetSalePrice() => _sellable.GetSalePrice();

        public Money GetSalePrice(Mass mass) => _sellable.GetSalePrice(mass);
    }
}
