using System.Collections.Generic;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class Order
    {
        private ICollection<IScannable> _scannedItems = new List<IScannable>();

        public long Id { get; set; }
        public IEnumerable<IScannable> ScannedItems { get { return _scannedItems; } }

        public Order AddScannable(IScannable scannable)
        {
            _scannedItems.Add(scannable);
            return this;
        }
    }
}