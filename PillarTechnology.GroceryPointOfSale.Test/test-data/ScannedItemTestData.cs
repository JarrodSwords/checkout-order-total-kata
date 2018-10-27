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
            var products = new ProductTestData().GetEnumerator();
            var weight = 0;

            while (products.MoveNext())
            {
                yield return products.Current.GetType() == typeof(Item) ?
                    new ItemFactory(products.Current).CreateScannable() :
                    new WeightedItemFactory(products.Current, ++weight).CreateScannable();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}