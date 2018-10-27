using System.Collections;
using System.Collections.Generic;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ProductTestData : IEnumerable<Product>
    {
        public IEnumerator<Product> GetEnumerator()
        {
            yield return new Product("can of soup");
            yield return new Product("frozen pizza");
            yield return new Product("lean ground beef", SellByType.Weight);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}