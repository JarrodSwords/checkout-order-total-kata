using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class RetailLineItem : LineItem
    {
        public int ScannedItemId { get; }

        public RetailLineItem(string description, Money salePrice, int scannedItemId) : base(description, salePrice)
        {
            ScannedItemId = scannedItemId;
        }
    }
}
