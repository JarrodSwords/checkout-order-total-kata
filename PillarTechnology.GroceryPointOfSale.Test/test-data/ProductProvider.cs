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
            new Product("frozen pizza", Money.USDollar(3m), SellByType.Unit, MarkdownProvider.GetMarkdown(Money.USDollar(0.25m), DateRange.Active)),
            new Product("peanut butter", Money.USDollar(2m), SellByType.Unit, MarkdownProvider.GetMarkdown(Money.USDollar(0.50m), DateRange.Expired)),
            new Product("jelly", Money.USDollar(2m), SellByType.Unit, MarkdownProvider.GetMarkdown(Money.USDollar(0.75m), DateRange.Future)),
            new Product("lean ground beef", Money.USDollar(2m), SellByType.Weight),
            new Product("honey ham", Money.USDollar(1.5m), SellByType.Weight, MarkdownProvider.GetMarkdown(Money.USDollar(0.25m), DateRange.Active)),
            new Product("sausage", Money.USDollar(1.5m), SellByType.Weight, MarkdownProvider.GetMarkdown(Money.USDollar(0.50m), DateRange.Active)),
            new Product("pepperoni", Money.USDollar(1.5m), SellByType.Weight, MarkdownProvider.GetMarkdown(Money.USDollar(0.75m), DateRange.Active))
        };

        public static ICollection<Product> GetOneOfEachProduct(decimal retailPriceAmount = 1m, decimal markdownAmount = 0.5m)
        {
            var retailPrice = Money.USDollar(retailPriceAmount);
            var markdown = Money.USDollar(markdownAmount);

            /// <remarks>
            /// code:
            ///     E, W - eaches/weighted product
            ///     M - markdown
            ///     S - special
            ///     xM, xS - expired markdown/special
            ///     fM, fS - future markdown/special
            /// </remarks>
            return new List<Product>
            {
                new Product("E", retailPrice, SellByType.Unit),
                new Product("E M", retailPrice, SellByType.Unit)
                {
                    Markdown = MarkdownProvider.GetMarkdown(markdown, DateRange.Active)
                },
                new Product("E xM", retailPrice, SellByType.Unit)
                {
                    Markdown = MarkdownProvider.GetMarkdown(markdown, DateRange.Expired)
                },
                new Product("E fM", retailPrice, SellByType.Unit)
                {
                    Markdown = MarkdownProvider.GetMarkdown(markdown, DateRange.Future)
                },
                new Product("W", retailPrice, SellByType.Weight),
                new Product("W M", retailPrice, SellByType.Weight)
                {
                    Markdown = MarkdownProvider.GetMarkdown(markdown, DateRange.Active)
                },
                new Product("W xM", retailPrice, SellByType.Weight)
                {
                    Markdown = MarkdownProvider.GetMarkdown(markdown, DateRange.Expired)
                },
                new Product("W fM", retailPrice, SellByType.Weight)
                {
                    Markdown = MarkdownProvider.GetMarkdown(markdown, DateRange.Future)
                }
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var product in ProductProvider.Products)
                yield return new object[] { product.Name };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
