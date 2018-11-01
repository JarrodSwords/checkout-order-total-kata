using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ScannedItemProvider : IEnumerable<IScannable>
    {
        private ICollection<IScannable> _scannedItems = new List<IScannable>();

        public ICollection<IScannable> ScannedItems => _scannedItems;

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

        public IScannable GetScannable()
        {
            return _scannedItems.First();
        }

        public IEnumerator<IScannable> GetEnumerator() => _scannedItems.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}