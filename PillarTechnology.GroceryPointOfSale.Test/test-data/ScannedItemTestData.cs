using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ScannedItemTestData : IEnumerable<IScannable>
    {
        private ICollection<IScannable> _scannedItems = new List<IScannable>();

        public ScannedItemTestData()
        {
            for (var i = 0; i < 5; i++)
                _scannedItems.Add(new ItemFactory(ProductProvider.GetProductSoldByUnit()).CreateScannable());

            _scannedItems.Add(new WeightedItemFactory(ProductProvider.GetProductSoldByWeight(), 2m).CreateScannable());
            _scannedItems.Add(new WeightedItemFactory(ProductProvider.GetProductSoldByWeight(), 1m).CreateScannable());
            _scannedItems.Add(new WeightedItemFactory(ProductProvider.GetProductSoldByWeight(), 1.5m).CreateScannable());
        }

        public IScannable GetScannable()
        {
            return _scannedItems.First();
        }

        public IEnumerator<IScannable> GetEnumerator() => _scannedItems.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}