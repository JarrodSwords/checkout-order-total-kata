using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PointOfSale.ApplicationServiceImplementations;
using PointOfSale.ApplicationServices;
using PointOfSale.Domain;
using PointOfSale.Infrastructure.InMemory;

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

            services.AddTransient<IMarkdownFactory, Markdown.Factory>();
            services.AddTransient<BuyNForXAmountSpecial.Factory>();
            services.AddTransient<BuyNGetMAtXPercentOffSpecial.Factory>();
            services.AddTransient<BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial.Factory>();

            services.AddTransient<IDateTimeProvider, BasicDateTimeProvider>();
            services.AddTransient<ProductSpecialConfigurationServiceProvider>();

            services.AddSingleton<IOrderRepository>(new InMemoryOrderRepository());
            services.AddSingleton<IProductRepository>(new InMemoryProductRepository());

            services.AddTransient<ICheckoutService, CheckoutService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<IProductConfigurationService, ProductConfigurationService>();
            services.AddTransient<IProductMarkdownConfigurationService, ProductMarkdownConfigurationService>();
            services.AddTransient<BuyNForXAmountConfigurationService>();
            services.AddTransient<BuyNGetMAtXPercentOffConfigurationService>();
            services.AddTransient<BuyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService>();

            services.AddTransient<RemoveScannedItemArgsValidator>();
            services.AddTransient<ScanItemArgsValidator>();
            services.AddTransient<ScanWeightedItemArgsValidator>();

            services.AddTransient<CreateProductArgsValidator>();
            services.AddTransient<UpdateProductArgsValidator>();

            services.AddTransient<UpsertProductMarkdownArgsValidator>();

            services.AddTransient<CreateBuyNForXAmountSpecialArgsValidator>();
            services.AddTransient<CreateBuyNGetMAtXPercentOffSpecialArgsValidator>();
            services.AddTransient<CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator>();
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
