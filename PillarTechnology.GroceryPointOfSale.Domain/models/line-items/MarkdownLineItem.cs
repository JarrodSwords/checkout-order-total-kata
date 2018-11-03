using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class MarkdownLineItem : LineItem
    {
        public int ScannedItemId { get; }

        public MarkdownLineItem(string description, Money salePrice, int scannedItemId) : base($"{description} markdown", salePrice)
        {
            ScannedItemId = scannedItemId;
        }
    }
}
