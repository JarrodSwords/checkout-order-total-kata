using System.Collections.Generic;

namespace GroceryPointOfSale.ApplicationServices
{
    public class InvoiceDto
    {
        public long OrderId { get; set; }
        public IEnumerable<LineItemDto> LineItems { get; set; }
        public decimal PreTaxTotal { get; set; }
    }
}
