using NodaMoney;

namespace GroceryPointOfSale.Domain
{
    /// <summary>
    /// A good available for sale
    /// </summary>
    public class Product
    {
        public bool HasActiveMarkdown => Markdown != null && Markdown.IsActive;
        public bool HasActiveSpecial => Special != null && Special.IsActive;
        public Markdown Markdown { get; set; }
        public string Name { get; }
        public Money RetailPrice { get; set; }
        public SellByType SellByType { get; set; }
        public Special Special { get; set; }

        public Product() { }

        public Product(string name, Money retailPrice, SellByType sellByType = SellByType.Unit, Markdown markdown = null)
        {
            Name = name;
            RetailPrice = retailPrice;
            SellByType = sellByType;
            Markdown = markdown;
        }

        public override string ToString() => $"{Name}; {RetailPrice}; {Markdown}";
    }
}
