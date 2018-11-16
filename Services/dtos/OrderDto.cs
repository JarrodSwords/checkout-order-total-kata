using System.Collections.Generic;
using PointOfSale.Domain;

namespace PointOfSale.ApplicationServices
{
    public class OrderDto
    {
        public long Id { get; set; }
        public IEnumerable<ScannedItemDto> ScannedItems { get; set; }
        public InvoiceDto Invoice { get; set; }
    }
}
