using Microsoft.AspNetCore.Mvc;
using PointOfSale.Services;

namespace PointOfSale.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;
        private readonly IOrderService _orderService;

        public OrderController(ICheckoutService checkoutService, IOrderService orderService)
        {
            _checkoutService = checkoutService;
            _orderService = orderService;
        }

        [Route("{orderId}")]
        [HttpGet]
        public ActionResult<OrderDto> FindOrder(long orderId)
        {
            return _orderService.FindOrder(orderId);
        }

        [Route("{orderId}/preTaxTotal")]
        [HttpGet]
        public ActionResult<decimal> GetOrderPreTaxTotal(long orderId)
        {
            return _orderService.GetInvoice(orderId).PreTaxTotal;
        }

        [Route("{orderId}/scannedItems")]
        [HttpPost]
        public ActionResult<IScannedItemDto> AddScannedItem(long orderId, [FromBody] ScanItemArgs args)
        {
            args.OrderId = orderId;
            return _checkoutService.ScanItem(args);
        }

        [Route("{orderId}/weightedScannedItems")]
        [HttpPost]
        public ActionResult<IScannedItemDto> AddWeightedScannedItem(long orderId, [FromBody] ScanWeightedItemArgs args)
        {
            args.OrderId = orderId;
            return _checkoutService.ScanWeightedItem(args);
        }

        [Route("{orderId}/scannedItems/{scannedItemId}")]
        [HttpDelete]
        public ActionResult<IScannedItemDto> RemoveScannedItem(long orderId, int scannedItemId)
        {
            var args = new RemoveScannedItemArgs
            {
                OrderId = orderId,
                ScannedItemId = scannedItemId
            };

            return _checkoutService.RemoveScannedItem(args);
        }
    }
}
