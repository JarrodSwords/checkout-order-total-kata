using System.Collections.Generic;
using PointOfSale.Services;

namespace PointOfSale.Implementations
{
    public interface ISpecialServiceProvider
    {
        IEnumerable<string> SpecialTypes { get; }

        SpecialService GetService(CreateSpecialArgs args);
    }
}
