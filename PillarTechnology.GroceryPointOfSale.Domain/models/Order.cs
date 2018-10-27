using System.Collections.Generic;
using System.Linq;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class Order
    {
        private ICollection<IScannable> _scannedItems = new List<IScannable>();

        public long Id { get; set; }
        public IEnumerable<IScannable> ScannedItems { get { return _scannedItems; } }

        public Order AddScannable(IScannable scannable)
        {
            scannable.Id = ScannableIdGenerator.Next(_scannedItems);
            _scannedItems.Add(scannable);
            return this;
        }
    }

    public class ScannableIdGenerator
    {
        public static int Next(IEnumerable<IScannable> scannedItems)
        {
            return scannedItems.Count() == 0 ? 1 : scannedItems.Max(x => x.Id) + 1;
        }
    }
}