using System.Collections;
using System.Collections.Generic;
using NodaMoney;
using PointOfSale.Domain;

namespace PointOfSale.Test.Domain
{
    public class ProductProvider : IEnumerable<object[]>
    {
        public static ICollection<Product> Products => new List<Product>
        {
            new Product("can of soup", Money.USDollar(0.5m), SellByType.Unit),
            new Product("frozen pizza", Money.USDollar(3m), SellByType.Unit, MarkdownProvider.GetMarkdown(DateRange.Active, 0.25m)),
            new Product("peanut butter", Money.USDollar(2m), SellByType.Unit, MarkdownProvider.GetMarkdown(DateRange.Expired, 0.50m)),
            new Product("jelly", Money.USDollar(2m), SellByType.Unit, MarkdownProvider.GetMarkdown(DateRange.Future, 0.75m)),
            new Product("lean ground beef", Money.USDollar(2m), SellByType.Weight),
            new Product("honey ham", Money.USDollar(1.5m), SellByType.Weight, MarkdownProvider.GetMarkdown(DateRange.Active, 0.25m)),
            new Product("sausage", Money.USDollar(1.5m), SellByType.Weight, MarkdownProvider.GetMarkdown(DateRange.Expired, 0.50m)),
            new Product("pepperoni", Money.USDollar(1.5m), SellByType.Weight, MarkdownProvider.GetMarkdown(DateRange.Future, 0.75m))
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
            ///     nM, nS - invalid (expired/future) markdown/special
            /// </remarks>
            return new List<Product>
            {
                new Product("E", retailPrice, SellByType.Unit),
                new Product("E M", retailPrice, SellByType.Unit)
                {
                    Markdown = MarkdownProvider.GetMarkdown(DateRange.Active, markdownAmount)
                },
                new Product("E nM", retailPrice, SellByType.Unit)
                {
                    Markdown = MarkdownProvider.GetMarkdown(DateRange.Expired, markdownAmount)
                },
                new Product("E S", retailPrice, SellByType.Unit)
                {
                    Special = SpecialProvider.GetBuyNGetMAtXPercentOffSpecial(DateRange.Active)
                },
                new Product("E nS", retailPrice, SellByType.Unit)
                {
                    Special = SpecialProvider.GetBuyNGetMAtXPercentOffSpecial(DateRange.Expired)
                },
                new Product("W", retailPrice, SellByType.Weight),
                new Product("W M", retailPrice, SellByType.Weight)
                {
                    Markdown = MarkdownProvider.GetMarkdown(DateRange.Active, markdownAmount)
                },
                new Product("W nM", retailPrice, SellByType.Weight)
                {
                    Markdown = MarkdownProvider.GetMarkdown(DateRange.Expired, markdownAmount)
                },
                new Product("W S", retailPrice, SellByType.Weight)
                {
                    Special = SpecialProvider.GetBuyNGetMAtXPercentOffSpecial(DateRange.Active)
                },
                new Product("W nS", retailPrice, SellByType.Weight)
                {
                    Special = SpecialProvider.GetBuyNGetMAtXPercentOffSpecial(DateRange.Expired)
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
