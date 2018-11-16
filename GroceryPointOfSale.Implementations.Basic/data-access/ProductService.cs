using AutoMapper;
using GroceryPointOfSale.ApplicationServices;
using GroceryPointOfSale.Domain;

namespace GroceryPointOfSale.ApplicationServiceImplementations
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
            return _mapper.Map<ProductDto>(product);
        }
    }
}