using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ScannedItemProvider : IEnumerable<IScannable>
    {
        private static ICollection<IScannable> _scannedItems = new List<IScannable>();

        public static ICollection<IScannable> ScannedItems => _scannedItems;

        static ScannedItemProvider()
        {
            CreateScannedItems();
        }

        private static void CreateScannedItems()
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

        public static IScannable GetScannable()
        {
            return ScannedItemProvider.ScannedItems.First();
        }

        public IEnumerator<IScannable> GetEnumerator() => _scannedItems.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}