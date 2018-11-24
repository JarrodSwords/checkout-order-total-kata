using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PointOfSale.Domain
{
    public interface ISpecial
    {
        IEnumerable<SpecialLineItem> CreateLineItems(IEnumerable<ScannedItem> scannedItems);
    }

    public abstract partial class Special : ISpecial, ITemporal
    {
        private readonly ITemporal _temporal;
        public abstract string Description { get; }
        public DateTime EndTime => _temporal.EndTime;
        public bool IsActive => _temporal.IsActive;
        public int? Limit { get; }
        public abstract int ScannedItemsRequired { get; }
        public DateTime StartTime => _temporal.StartTime;

        protected Special(ITemporal temporal, int? limit = null)
        {
            _temporal = temporal;
            Limit = limit;
        }

        public abstract Money CalculateTotalDiscount(IEnumerable<ScannedItem> scannedItems);

        public virtual LineItem CreateLineItem(Product product, IEnumerable<ScannedItem> scannedItems, int skipMultiplier)
        {
            return new SpecialLineItem(product.Name, CalculateTotalDiscount(scannedItems), GetScannedItemIds(scannedItems, skipMultiplier), Description);
        }

        public abstract IEnumerable<int> GetScannedItemIds(IEnumerable<ScannedItem> scannedItems, int skipMultiplier);

        public IEnumerable<SpecialLineItem> CreateLineItems(IEnumerable<ScannedItem> scannedItems)
        {
            if (scannedItems.Count() == 0)
                yield break;

            var product = scannedItems.First().Product;
            var specials = scannedItems.Count() / ScannedItemsRequired;

            if (Limit.HasValue)
                specials = Math.Min(specials, Limit.Value / ScannedItemsRequired);

            for (var i = 0; i < specials; i++)
            {
                var specialScannedItems = scannedItems
                    .Skip(i * ScannedItemsRequired)
                    .Take(ScannedItemsRequired);

                var salePrice = CalculateTotalDiscount(specialScannedItems);

                if (salePrice > 0)
                    continue;

                if (product.HasActiveMarkdown)
                {
                    var markdown = ScannedItemsRequired * -product.Markdown.AmountOffRetail;
                    if (markdown <= salePrice)
                        continue;
                }

                yield return new SpecialLineItem(product.Name, salePrice, specialScannedItems.Select(x => x.Id), Description);
            }
        }
    }
}
