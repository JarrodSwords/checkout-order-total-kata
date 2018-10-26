using System.Collections.Generic;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class Order
    {
        private ICollection<IPurchasable> _scannedItems = new List<IPurchasable>();

        public long Id { get; set; }
        public IEnumerable<IPurchasable> ScannedItems { get { return _scannedItems; } }

        public Order AddPurchasable(IPurchasable purchasable)
        {
            _scannedItems.Add(purchasable);
            return this;
        }
    }
}