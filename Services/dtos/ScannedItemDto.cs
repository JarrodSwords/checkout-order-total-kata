namespace PointOfSale.Services
{
    public interface IScannedItemDto
    {
        int Id { get; set; }
        IProductDto Product { get; set; }
    }

    public class ScannedItemDto : IScannedItemDto
    {
        public int Id { get; set; }
        public IProductDto Product { get; set; }
    }

    public class ScannedItemAsEachesDto : ScannedItemDto
    {
    }

    public class ScannedItemWithMassDto : ScannedItemDto
    {
        public IMassDto Mass { get; set; }
    }
}
