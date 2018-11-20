using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations
{
    public abstract class ProductFactory : IFactory<Product>
    {
        protected readonly IProductNameArgs _args;
        protected readonly IMapper _mapper;

        public ProductFactory(IProductNameArgs args, IMapper mapper)
        {
            _args = args;
            _mapper = mapper;
        }

        public abstract Product Create();
        public abstract ProductDto CreateProductDto(Product product);
        public abstract Product UpdateProduct(Product product);
    }
}
