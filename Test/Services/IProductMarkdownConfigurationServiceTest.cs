using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using PointOfSale.Domain;
using PointOfSale.Services;
using PointOfSale.Test.Domain;
using Xunit;

namespace PointOfSale.Test.Services
{
    public abstract class IProductMarkdownConfigurationServiceTest
    {
        protected readonly DateTime _now = DependencyProvider.CreateDateTimeProvider().Now;
        protected IProductMarkdownConfigurationService _productMarkdownConfigurationService;

        [Theory]
        [ClassData(typeof(UpsertProductMarkdownData))]
        public void UpsertProductMarkdown_UpsertsProductMarkdown(decimal amountOffRetail, DateTime? endTime, string productName, string sellByType, DateTime? startTime)
        {
            var args = new UpsertProductMarkdownArgs(amountOffRetail, endTime, productName, sellByType, startTime);

            var persistedProduct = _productMarkdownConfigurationService.UpsertProductMarkdown(args);

            persistedProduct.Name.Should().Be(args.ProductName);
            persistedProduct.Markdown.AmountOffRetail.Should().Be(args.AmountOffRetail);
            persistedProduct.Markdown.StartTime.Should().Be(args.StartTime.Value);
            persistedProduct.Markdown.EndTime.Should().Be(args.EndTime.Value);
        }

        public class UpsertProductMarkdownData : IEnumerable<object[]>
        {
            private readonly DateTime _now = DependencyProvider.CreateDateTimeProvider().Now;

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0.1m, _now.EndOfWeek(), "can of soup", "eaches", _now.StartOfWeek() };
                yield return new object[] { 0.1m, _now.EndOfWeek(), "lean ground beef", "mass", _now.StartOfWeek() };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
