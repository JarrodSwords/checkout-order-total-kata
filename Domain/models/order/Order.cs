using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace PointOfSale.Domain
{
    public class Order
    {
        private readonly ObservableCollection<ScannedItem> _scannedItems = new ObservableCollection<ScannedItem>();

        public long Id { get; set; }
        public IEnumerable<ScannedItem> ScannedItems => _scannedItems;
        public Invoice Invoice { get; private set; }

        public Order()
        {
            _scannedItems.CollectionChanged += ScannedItemsChanged;
        }

        public Order AddScannedItem(ScannedItem scannedItem)
        {
            scannedItem.Id = ScannedItemIdGenerator.Next(_scannedItems);
            _scannedItems.Add(scannedItem);
            return this;
        }

        public ScannedItem RemoveScannedItem(int itemId)
        {
            var itemToRemove = _scannedItems.Single(x => x.Id == itemId);
            _scannedItems.Remove(itemToRemove);
            return itemToRemove;
        }

        public void ScannedItemsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            Invoice = new Invoice.Factory(this).CreateInvoice();
        }

        private class ScannedItemIdGenerator
        {
            public static int Next(IEnumerable<ScannedItem> scannedItems)
            {
                return scannedItems.Count() == 0 ? 1 : scannedItems.Max(x => x.Id) + 1;
            }
        }
    }
}
