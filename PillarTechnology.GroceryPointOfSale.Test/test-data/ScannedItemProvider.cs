using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ScannedItemProvider : IEnumerable<ScannedItem>
    {
        private ICollection<ScannedItem> _scannedItems = new List<ScannedItem>();

        public ICollection<ScannedItem> ScannedItems => _scannedItems;

        public ScannedItemProvider()
        {
            CreateScannedItems();
        }

        private void CreateScannedItems()
        {
            var itemFactory = new ItemFactory();
            var weightedItemFactory = new WeightedItemFactory();
            var weight = 0.5m;
            var weightIncrement = 0.5m;

            foreach (var product in ProductProvider.Products)
            {
                if (product.SellByType == SellByType.Unit)
                {
                    itemFactory.Configure(product);
                    _scannedItems.Add(itemFactory.CreateScannable());
                    continue;
                }

                weightedItemFactory.Configure(product, weight);
                _scannedItems.Add(weightedItemFactory.CreateScannable());
                weight += weightIncrement;
            }
        }

        public ScannedItem GetScannedItem()
        {
            return _scannedItems.First();
        }

        public IEnumerator<ScannedItem> GetEnumerator() => _scannedItems.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}