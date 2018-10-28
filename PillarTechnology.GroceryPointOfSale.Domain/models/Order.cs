using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class Order
    {
        private readonly ObservableCollection<IScannable> _scannedItems = new ObservableCollection<IScannable>();

        public long Id { get; set; }
        public IEnumerable<IScannable> ScannedItems { get { return _scannedItems; } }
        public Invoice Invoice { get; private set; }

        public Order()
        {
            _scannedItems.CollectionChanged += ScannedItemsChanged;
        }

        public Order AddScannable(IScannable scannable)
        {
            scannable.Id = ScannedItemIdGenerator.Next(_scannedItems);
            _scannedItems.Add(scannable);
            return this;
        }

        public IScannable RemoveScannedItem(int itemId)
        {
            var itemToRemove = _scannedItems.Single(x => x.Id == itemId);
            _scannedItems.Remove(itemToRemove);
            return itemToRemove;
        }

        public void ScannedItemsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            Invoice = new Invoice(this);
        }

        private class ScannedItemIdGenerator
        {
            public static int Next(IEnumerable<IScannable> scannedItems)
            {
                return scannedItems.Count() == 0 ? 1 : scannedItems.Max(x => x.Id) + 1;
            }
        }
    }
}