using System.Collections.Generic;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class OrderDto
    {
        public long Id { get; set; }
        public IEnumerable<ScannedItemDto> ScannedItems { get; set; }
        public InvoiceDto Invoice { get; set; }
    }
}
