using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public ProductDto FindProduct(string productName)
        {
            var product = _productRepository.FindProduct(productName);
            return product.GetType() == typeof(EachesProduct) ?
                (ProductDto) _mapper.Map<EachesProductDto>(product) :
                (ProductDto) _mapper.Map<MassProductDto>(product);
        }
    }
}
