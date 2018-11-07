using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public abstract class LineItem
    {
        public virtual string Description { get { return ProductName; } }
        public string ProductName { get; }
        public Money SalePrice { get; }

        public LineItem(string productName, Money salePrice)
        {
            ProductName = productName;
            SalePrice = salePrice;
        }

        public override string ToString() => $"{Description} - {SalePrice}";
    }
}
