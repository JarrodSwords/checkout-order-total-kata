using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public abstract class ProductHelperService : IFactory<Product>, IUpdate<Product>
    {
        protected readonly UpsertProductArgs _args;
        protected readonly IMapper _mapper;

        public ProductHelperService(UpsertProductArgs args, IMapper mapper)
        {
            _args = args;
            _mapper = mapper;
        }

        public abstract Product Create();
        public abstract ProductDto ToDto(Product product);
        public abstract Product Update(Product product);
    }
}
