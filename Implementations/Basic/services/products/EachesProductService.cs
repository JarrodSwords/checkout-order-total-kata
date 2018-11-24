using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class EachesProductService : ProductHelperService
    {
        public EachesProductService(UpsertProductArgs args, IMapper mapper) : base(args, mapper) { }

        public override Product Create()
        {
            var args = (IUpsertEachesProductArgs) _args;

            return new EachesProduct(
                args.ProductName,
                args.RetailPrice.Value
            );
        }

        public override ProductDto ToDto(Product product) =>
            _mapper.Map<EachesProductDto>((EachesProduct) product);

        public override Product Update(Product product)
        {
            var eachesProduct = Create();
            eachesProduct.Markdown = product.Markdown;
            eachesProduct.Special = product.Special;
            return eachesProduct;
        }
    }
}
