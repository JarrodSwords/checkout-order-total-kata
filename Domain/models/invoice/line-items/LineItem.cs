using NodaMoney;

namespace PointOfSale.Domain
{
    public abstract class LineItem
    {
        public virtual string Description => ProductName;
        public string ProductName { get; }
        public Money SalePrice { get; }

        public LineItem(string productName, Money salePrice)
        {
            ProductName = productName;
            SalePrice = salePrice;
        }
    }
}
