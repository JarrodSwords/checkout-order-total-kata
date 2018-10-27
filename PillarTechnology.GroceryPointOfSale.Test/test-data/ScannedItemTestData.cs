using System;
using System.Collections;
using System.Collections.Generic;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ScannedItemTestData : IEnumerable<IScannable>
    {
        public IEnumerator<IScannable> GetEnumerator()
        {
            var weight = 0;

            foreach (var product in new ProductTestData().Products)
            {
                yield return product.GetType() == typeof(Item) ?
                    new ItemFactory(product).CreateScannable() :
                    new WeightedItemFactory(product, ++weight).CreateScannable();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}