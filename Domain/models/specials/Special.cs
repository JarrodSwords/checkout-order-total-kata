using System;
using System.Collections.Generic;
using NodaMoney;

namespace PointOfSale.Domain
{
    public abstract partial class Special : ITemporal
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

        public abstract Money CalculateTotalDiscount(Product product);

        public virtual LineItem CreateLineItem(Product product, IEnumerable<ScannedItem> scannedItems, int skipMultiplier)
        {
            return new SpecialLineItem(product.Name, CalculateTotalDiscount(product), GetScannedItemIds(scannedItems, skipMultiplier), Description);
        }
        
        public abstract IEnumerable<int> GetScannedItemIds(IEnumerable<ScannedItem> scannedItems, int skipMultiplier);
    }
}
