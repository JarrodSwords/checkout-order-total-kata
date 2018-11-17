using PointOfSale.Domain;

namespace PointOfSale.Test.Domain
{
    public class DependencyProvider
    {
        public static IDateTimeProvider CreateDateTimeProvider() => new BasicDateTimeProvider();
    }
}
