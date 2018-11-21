using System.Collections.Generic;
using PointOfSale.Domain;

namespace PointOfSale.Services
{
    public class OrderDto
    {
        public long Id { get; set; }
        public IEnumerable<IScannedItemDto> ScannedItems { get; set; }
    }
}
