using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations
{
    public abstract class ProductService : IFactory<Product>, IUpdate<Product>
    {
        protected readonly UpsertProductArgs _args;
        protected readonly IMapper _mapper;

        public ProductService(UpsertProductArgs args, IMapper mapper)
        {
            _args = args;
            _mapper = mapper;
        }

        public abstract Product Create();
        public abstract ProductDto ToDto(Product product);
        public abstract Product Update(Product product);
    }
}
