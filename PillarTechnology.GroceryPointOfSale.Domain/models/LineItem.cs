using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class LineItem
    {
        public string ProductName { get; }
        public Money SalePrice { get; }
        public int? ScannedItemId { get; set; }

        public LineItem(string productName, Money salePrice, int? scannedItemId = null)
        {
            ScannedItemId = scannedItemId;
            ProductName = productName;
            SalePrice = salePrice;
        }

        public override string ToString() => $"Scanned item: {ScannedItemId}, Product: {ProductName}, Sale price: {SalePrice}";
    }
}