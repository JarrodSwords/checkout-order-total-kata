using System;
using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations
{
    public class EachesProductFactory : ProductFactory
    {
        public EachesProductFactory(IProductNameArgs args, IMapper mapper) : base(
            (IUpsertEachesProductArgs) args,
            mapper
        ) { }

        public override Product Create()
        {
            var args = (IUpsertEachesProductArgs) _args;

            return new EachesProduct(
                args.ProductName,
                args.RetailPrice.Value
            );
        }

        public override ProductDto CreateProductDto(Product product) =>
            _mapper.Map<EachesProductDto>(product);

        public override Product UpdateProduct(Product product)
        {
            var eachesProduct = Create();
            eachesProduct.Markdown = product.Markdown;
            eachesProduct.Special = product.Special;
            return eachesProduct;
        }
    }
}
