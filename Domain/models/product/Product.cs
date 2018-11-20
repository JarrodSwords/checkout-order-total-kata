using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    /// <summary>
    /// A good available for sale
    /// </summary>
    public abstract class Product : ISellableAsEaches, ISellableByMass
    {
        private readonly ISellableAsEaches _sellableAsEaches;
        private readonly ISellableByMass _sellableByMass;

        public bool HasActiveMarkdown => Markdown != null && Markdown.IsActive;
        public bool HasActiveSpecial => Special != null && Special.IsActive;
        public Markdown Markdown { get; set; }
        public Mass Mass
        {
            get => _sellableByMass.Mass;
            set => _sellableByMass.Mass = value;
        }
        public string Name { get; }
        public Money RetailPrice
        {
            get => _sellableAsEaches.RetailPrice;
            set => _sellableAsEaches.RetailPrice = value;
        }
        public Money RetailPricePerUnit
        {
            get => _sellableByMass.RetailPricePerUnit;
            set => _sellableByMass.RetailPricePerUnit = value;
        }
        public Special Special { get; set; }

        public Product(string name, ISellableAsEaches sellableAsEaches, ISellableByMass sellableByMass)
        {
            Name = name;
            _sellableAsEaches = sellableAsEaches;
            _sellableByMass = sellableByMass;
        }
    }
}
