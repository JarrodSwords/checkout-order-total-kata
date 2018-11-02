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
            var weight = 0.5m;
            var weightIncrement = 0.5m;

            foreach (var product in ProductProvider.Products)
            {
                if (product.SellByType == SellByType.Unit)
                {
                    _scannedItems.Add(new ScannedItem(product));
                    continue;
                }

                _scannedItems.Add(new WeightedScannedItem(product, weight));
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