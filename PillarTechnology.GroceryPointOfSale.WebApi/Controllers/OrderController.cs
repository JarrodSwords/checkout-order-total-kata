using Microsoft.AspNetCore.Mvc;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;

namespace PillarTechnology.GroceryPointOfSale.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public OrderController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [Route("{orderId}/scannedItems")]
        [HttpPost]
        public ActionResult<ScannedItemDto> AddScannedItem(long orderId, [FromBody] ScanItemArgs args)
        {
            return _checkoutService.ScanItem(args);
        }

        [Route("{orderId}/weightedScannedItems")]
        [HttpPost]
        public ActionResult<ScannedItemDto> AddWeightedScannedItem(long orderId, [FromBody] ScanWeightedItemArgs args)
        {
            return _checkoutService.ScanWeightedItem(args);
        }

        [Route("{orderId}/scannedItems")]
        [HttpDelete]
        public ActionResult<ScannedItemDto> RemoveScannedItem(long orderId, [FromBody] RemoveScannedItemArgs args)
        {
            return _checkoutService.RemoveScannedItem(args);
        }
    }
}
