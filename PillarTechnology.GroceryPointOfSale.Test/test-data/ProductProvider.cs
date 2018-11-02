using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ProductProvider : IEnumerable<object[]>
    {
        public static ICollection<Product> Products => new List<Product>
        {
            new Product("can of soup", Money.USDollar(0.5m), SellByType.Unit),
            new Product("frozen pizza", Money.USDollar(3m), SellByType.Unit, MarkdownProvider.ActiveMarkdown(0.25m)),
            new Product("peanut butter", Money.USDollar(2m), SellByType.Unit, MarkdownProvider.ExpiredMarkdown(0.5m)),
            new Product("jelly", Money.USDollar(2m), SellByType.Unit, MarkdownProvider.FutureMarkdown(0.75m)),
            new Product("lean ground beef", Money.USDollar(2m), SellByType.Weight),
            new Product("honey ham", Money.USDollar(1.5m), SellByType.Weight, MarkdownProvider.ActiveMarkdown(0.25m)),
            new Product("sausage", Money.USDollar(1.5m), SellByType.Weight, MarkdownProvider.ExpiredMarkdown(0.5m)),
            new Product("pepperoni", Money.USDollar(1.5m), SellByType.Weight, MarkdownProvider.FutureMarkdown(0.75m))
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var product in ProductProvider.Products)
                yield return new object[] { product.Name };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}