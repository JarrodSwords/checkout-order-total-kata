using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PointOfSale.Domain
{
    public abstract partial class Special : ISpecial, ITemporal
    {
        private readonly ITemporal _temporal;
        public abstract string Description { get; }
        public DateTime EndTime => _temporal.EndTime;
        public bool IsActive => _temporal.IsActive;
        public int? Limit { get; }
        public abstract int ScannedItemsRequired { get; }
        public DateTime StartTime => _temporal.StartTime;

        protected Special(DateTime endTime, DateTime startTime, int? limit = null)
        {
            _temporal = new Temporal(endTime, startTime);
            Limit = limit;
        }

        public abstract Money CalculateTotalDiscount(IEnumerable<ScannedItem> scannedItems);

        public IEnumerable<SpecialLineItem> CreateLineItems(IEnumerable<ScannedItem> scannedItems)
        {
            if (scannedItems.Count() == 0)
                yield break;

            var product = scannedItems.First().Product;
            var specials = scannedItems.Count() / ScannedItemsRequired; // possible applicable specials

            // specials applied cannot exceed item limit
            if (Limit.HasValue)
                specials = Math.Min(specials, Limit.Value / ScannedItemsRequired);

            for (var i = 0; i < specials; i++)
            {
                var specialScannedItems = scannedItems
                    .Skip(i * ScannedItemsRequired)
                    .Take(ScannedItemsRequired);

                var specialDiscount = CalculateTotalDiscount(specialScannedItems);

                if (specialDiscount > 0)
                    continue;

                if (product.HasActiveMarkdown)
                {
                    var markdownDiscount = ScannedItemsRequired * -product.Markdown.AmountOffRetail;
                    if (markdownDiscount <= specialDiscount)
                        continue;
                }

                yield return new SpecialLineItem(
                    product.Name,
                    specialDiscount,
                    specialScannedItems.Select(x => x.Id),
                    Description
                );
            }
        }
    }
}
