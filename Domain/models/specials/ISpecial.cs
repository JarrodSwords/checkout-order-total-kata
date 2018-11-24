using System.Collections.Generic;

namespace PointOfSale.Domain
{
    public interface ISpecial
    {
        IEnumerable<SpecialLineItem> CreateLineItems(IEnumerable<ScannedItem> scannedItems);
    }
}
