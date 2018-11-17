namespace PointOfSale.Services
{
    public interface IOrderService
    {
        OrderDto FindOrder(long orderId);
    }
}
