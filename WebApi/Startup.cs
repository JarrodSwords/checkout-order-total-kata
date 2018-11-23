using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PointOfSale.Domain;
using PointOfSale.Implementations.Basic;
using PointOfSale.Infrastructure.InMemory;
using PointOfSale.Services;

namespace PointOfSale.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())));

            services.AddTransient<BuyNForXAmountSpecial.Factory>();
            services.AddTransient<BuyNGetMAtXPercentOffSpecial.Factory>();
            services.AddTransient<BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial.Factory>();

            services.AddTransient<IDateTimeProvider, BasicDateTimeProvider>();

            services.AddSingleton<IOrderRepository>(new InMemoryOrderRepository());
            services.AddSingleton<IProductRepository>(new InMemoryProductRepository());

            services.AddTransient<ICheckoutService, CheckoutService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductServiceProvider, ProductServiceProvider>();
            services.AddTransient<ISpecialServiceProvider, SpecialServiceProvider>();

            services.AddTransient<IProductConfigurationService, ProductConfigurationService>();
            services.AddTransient<IProductMarkdownConfigurationService, ProductMarkdownConfigurationService>();
            services.AddTransient<IProductSpecialConfigurationService, ProductSpecialConfigurationService>();

            services.AddTransient<AmountOffRetailValidator>();
            services.AddTransient<DiscountedItemsValidator>();
            services.AddTransient<GroupSalePriceValidator>();
            services.AddTransient<IsEachesProductValidator>();
            services.AddTransient<IsMassProductValidator>();
            services.AddTransient<LimitValidator>();
            services.AddTransient<OrderMustExistValidator>();
            services.AddTransient<PercentageOffValidator>();
            services.AddTransient<PreDiscountItemsValidator>();
            services.AddTransient<ProductMustExistValidator>();
            services.AddTransient<ProductMustNotExistValidator>();
            services.AddTransient<MassValidator>();
            services.AddTransient<SellByTypeValidator>();
            services.AddTransient<TemporalValidator>();

            services.AddTransient<RemoveScannedItemArgsValidator>();
            services.AddTransient<ScanItemArgsValidator>();
            services.AddTransient<ScanWeightedItemArgsValidator>();

            services.AddTransient<CreateProductArgsValidator>();
            services.AddTransient<UpdateProductArgsValidator>();
            services.AddTransient<UpsertProductMarkdownArgsValidator>();
            services.AddTransient<CreateSpecialArgsValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
