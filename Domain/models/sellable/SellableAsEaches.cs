using System;
using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public class SellableAsEaches : ISellable
    {
        public EachesProduct EachesProduct { get; }

        public SellableAsEaches(EachesProduct eachesProduct)
        {
            EachesProduct = eachesProduct;
        }

        public Money GetSalePrice() => EachesProduct.RetailPrice;
    }
}
