using System;
using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;
using static PointOfSale.Domain.MassProduct;

namespace PointOfSale.Implementations
{
    public class MassProductFactory : ProductFactory
    {
        public MassProductFactory(IUpsertProductArgs args, IMapper mapper) : base(
            (IUpsertMassProductArgs) args,
            mapper
        ) { }

        public override Product Create()
        {
            var args = (IUpsertMassProductArgs) _args;
            var builder = new MassProductBuilder(args.Name, args.RetailPricePerUnit.Value);

            if ((args.MassAmount.HasValue && args.MassAmount > 0) && !String.IsNullOrWhiteSpace(args.MassUnit))
                builder.SetMass(args.MassAmount.Value, args.MassUnit);

            return builder.Build();
        }

        public override ProductDto CreateProductDto(Product product) =>
            _mapper.Map<MassProductDto>(product);

        public override Product UpdateProduct(Product product)
        {
            var massProduct = Create();
            massProduct.Markdown = product.Markdown;
            massProduct.Special = product.Special;
            return massProduct;
        }
    }
}
