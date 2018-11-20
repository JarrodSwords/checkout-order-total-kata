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
            new EachesProduct("can of soup", 0.5m),
            new EachesProduct("frozen pizza", 3m) { Markdown = MarkdownProvider.GetMarkdown(DateRange.Active, 0.25m) },
            new EachesProduct("peanut butter", 2m) { Markdown = MarkdownProvider.GetMarkdown(DateRange.Expired, 0.50m) },
            new EachesProduct("jelly", 2m) { Markdown = MarkdownProvider.GetMarkdown(DateRange.Future, 0.75m) },
            new MassProduct("lean ground beef", 2m),
            new MassProduct("honey ham", 1.5m) { Markdown = MarkdownProvider.GetMarkdown(DateRange.Active, 0.25m) },
            new MassProduct("sausage", 1.5m) { Markdown = MarkdownProvider.GetMarkdown(DateRange.Expired, 0.50m) },
            new MassProduct("pepperoni", 1.5m) { Markdown = MarkdownProvider.GetMarkdown(DateRange.Future, 0.75m) }
        };

        public static ICollection<Product> GetOneOfEachProduct(decimal retailPriceAmount = 1m, decimal markdownAmount = 0.5m)
        {
            var retailPrice = retailPriceAmount;
            var markdown = markdownAmount;

            /// <remarks>
            /// code:
            ///     E, W - eaches/weighted product
            ///     M - markdown
            ///     S - special
            ///     nM, nS - invalid (expired/future) markdown/special
            /// </remarks>
            return new List<Product>
            {
                new EachesProduct("E", retailPrice),
                new EachesProduct("E M", retailPrice)
                {
                    Markdown = MarkdownProvider.GetMarkdown(DateRange.Active, markdownAmount)
                },
                new EachesProduct("E nM", retailPrice)
                {
                    Markdown = MarkdownProvider.GetMarkdown(DateRange.Expired, markdownAmount)
                },
                new EachesProduct("E S", retailPrice)
                {
                    Special = SpecialProvider.GetBuyNGetMAtXPercentOffSpecial(DateRange.Active)
                },
                new EachesProduct("E nS", retailPrice)
                {
                    Special = SpecialProvider.GetBuyNGetMAtXPercentOffSpecial(DateRange.Expired)
                },
                new MassProduct("W", retailPrice),
                new MassProduct("W M", retailPrice)
                {
                    Markdown = MarkdownProvider.GetMarkdown(DateRange.Active, markdownAmount)
                },
                new MassProduct("W nM", retailPrice)
                {
                    Markdown = MarkdownProvider.GetMarkdown(DateRange.Expired, markdownAmount)
                },
                new MassProduct("W S", retailPrice)
                {
                    Special = SpecialProvider.GetBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(DateRange.Active)
                },
                new MassProduct("W nS", retailPrice)
                {
                    Special = SpecialProvider.GetBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(DateRange.Expired)
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
