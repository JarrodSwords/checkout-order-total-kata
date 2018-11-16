namespace PointOfSale.ApplicationServices
{
    public interface IOrderService
    {
        OrderDto FindOrder(long orderId);
    }
}
