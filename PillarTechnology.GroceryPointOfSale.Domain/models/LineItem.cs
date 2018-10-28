using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class LineItem
    {
        public string ProductName { get; }
        public Money SalePrice { get; }

        public LineItem(string productName, Money salePrice)
        {
            ProductName = productName;
            SalePrice = salePrice;
        }
    }
}