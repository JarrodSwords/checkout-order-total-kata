using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class MarkdownLineItem : LineItem
    {
        public override string Description => $"{ProductName} - markdown";
        public int ScannedItemId { get; }

        public MarkdownLineItem(string productName, Money salePrice, int scannedItemId) : base(productName, salePrice)
        {
            ScannedItemId = scannedItemId;
        }
    }
}
