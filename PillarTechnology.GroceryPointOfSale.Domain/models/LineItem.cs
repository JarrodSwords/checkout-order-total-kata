using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public abstract class LineItem
    {
        public string Description { get; }
        public Money SalePrice { get; }

        public LineItem(string description, Money salePrice)
        {
            Description = description;
            SalePrice = salePrice;
        }

        public override string ToString() => $"{Description} - {SalePrice}";
    }
}
