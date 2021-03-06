using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public abstract class IProductMarkdownConfigurationServiceTest
    {
        protected readonly DateTime _now = DateTime.Now;
        protected IProductMarkdownConfigurationService _productMarkdownConfigurationService;

        [Theory]
        [ClassData(typeof(UpsertProductMarkdownData))]
        public void UpsertProductMarkdown_UpsertsProductMarkdown(string productName, decimal amountOffRetail, DateTime? startTime, DateTime? endTime)
        {
            var args = new UpsertProductMarkdownArgs(productName, amountOffRetail, startTime, endTime);

            var persistedProduct = _productMarkdownConfigurationService.UpsertProductMarkdown(args);

            persistedProduct.Name.Should().Be(args.ProductName);
            persistedProduct.Markdown.AmountOffRetail.Should().Be(args.AmountOffRetail);
            persistedProduct.Markdown.StartTime.Should().Be(args.StartTime.Value);
            persistedProduct.Markdown.EndTime.Should().Be(args.EndTime.Value);
        }

        public class UpsertProductMarkdownData : IEnumerable<object[]>
        {
            private readonly DateTime _now = DateTime.Now;

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "can of soup", 0.1m, _now.StartOfWeek(), _now.EndOfWeek() };
                yield return new object[] { "lean ground beef", 0.1m, _now.StartOfWeek(), _now.EndOfWeek() };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
