using AutoMapper;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class BuyNGetMAtXPercentOffConfigurationService : ProductSpecialConfigurationService<CreateBuyNGetMAtXPercentOffSpecialArgs>
    {
        private readonly BuyNGetMAtXPercentOffSpecial.Factory _factory;

        public BuyNGetMAtXPercentOffConfigurationService(
            IMapper mapper,
            IProductRepository productRepository,
            BuyNGetMAtXPercentOffSpecial.Factory factory,
            CreateBuyNGetMAtXPercentOffSpecialArgsValidator validator
        ) : base(mapper, productRepository, validator)
        {
            _factory = factory;
        }

        public override ISpecialFactory GetConfiguredSpecialFactory(CreateBuyNGetMAtXPercentOffSpecialArgs args)
        {
            return _factory.Configure(
                args.DiscountedItems.Value,
                args.EndTime.Value,
                args.PercentageOff.Value,
                args.PreDiscountItems.Value,
                args.StartTime.Value,
                args.Limit
            );
        }

        public override ISpecialDto CreateSpecialDto(Special special)
        {
            return _mapper.Map<BuyNGetMAtXPercentOffSpecialDto>(special);
        }

        public override string GetProductName(CreateBuyNGetMAtXPercentOffSpecialArgs args)
        {
            return args.ProductName;
        }
    }
}
