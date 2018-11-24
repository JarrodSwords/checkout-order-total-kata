using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    /// <summary>
    /// A good available for sale
    /// </summary>
    public abstract class Product
    {
        public bool HasActiveMarkdown => Markdown != null && Markdown.IsActive;
        public bool HasActiveSpecial => Special != null && Special.IsActive;
        public Markdown Markdown { get; set; }
        public string Name { get; }
        public Money RetailPrice { get; set; }
        public Special Special { get; set; }

        public Product(string name, decimal retailPrice)
        {
            Name = name;
            RetailPrice = retailPrice;
        }
    }
}
