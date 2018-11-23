using System;
using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;
using static PointOfSale.Domain.MassProduct;

namespace PointOfSale.Implementations.Basic
{
    public class MassProductService : ProductHelperService
    {
        public MassProductService(UpsertProductArgs args, IMapper mapper) : base(args, mapper) { }

        public override Product Create()
        {
            var args = (IUpsertMassProductArgs) _args;
            var builder = new MassProductBuilder(args.ProductName, args.RetailPrice.Value);

            if ((args.MassAmount.HasValue && args.MassAmount > 0) && !String.IsNullOrWhiteSpace(args.MassUnit))
                builder.SetMass(args.MassAmount.Value, args.MassUnit);

            return builder.Build();
        }

        public override ProductDto ToDto(Product product) =>
            _mapper.Map<MassProductDto>((MassProduct) product);

        public override Product Update(Product product)
        {
            var massProduct = Create();
            massProduct.Markdown = product.Markdown;
            massProduct.Special = product.Special;
            return massProduct;
        }
    }
}
