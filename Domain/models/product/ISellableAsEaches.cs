using System;
using NodaMoney;

namespace PointOfSale.Domain
{
    public interface ISellableAsEaches
    {
        Money RetailPrice { get; set; }
    }

    public class SellableAsEaches : ISellableAsEaches
    {
        public Money RetailPrice { get; set; }
    }

    public class NotSellableAsEaches : ISellableAsEaches
    {
        public Money RetailPrice
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
