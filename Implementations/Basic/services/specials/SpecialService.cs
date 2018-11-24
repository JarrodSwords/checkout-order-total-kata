using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public abstract class SpecialService : IFactory<Special>
    {
        protected readonly CreateSpecialArgs _args;
        protected readonly IDateTimeProvider _dateTimeProvider;
        protected readonly IMapper _mapper;

        public SpecialService(
            CreateSpecialArgs args,
            IDateTimeProvider dateTimeProvider,
            IMapper mapper
        )
        {
            _args = args;
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
        }

        public abstract Special Create();
        public abstract ISpecialDto ToDto(Special special);
    }
}
