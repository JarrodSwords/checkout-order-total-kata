using System;

namespace PointOfSale.Services
{
    public interface ITemporalArgs
    {
        DateTime? EndTime { get; set; }
        DateTime? StartTime { get; set; }
    }
}
