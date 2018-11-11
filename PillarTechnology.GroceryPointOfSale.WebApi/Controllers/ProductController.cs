using Microsoft.AspNetCore.Mvc;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;

namespace PillarTechnology.GroceryPointOfSale.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductConfigurationService _productConfigurationService;
        private readonly IProductMarkdownConfigurationService _productMarkdownConfigurationService;
        private readonly IProductService _productService;

        public ProductController(
            IProductConfigurationService productConfigurationService,
            IProductMarkdownConfigurationService productMarkdownConfigurationService,
            IProductService productService
        )
        {
            _productConfigurationService = productConfigurationService;
            _productMarkdownConfigurationService = productMarkdownConfigurationService;
            _productService = productService;
        }

        [HttpPost]
        public ActionResult<ProductDto> CreateProduct([FromBody] UpsertProductArgs upsertProductArgs)
        {
            return _productConfigurationService.CreateProduct(upsertProductArgs);
        }

        [Route("{productName}")]
        [HttpGet]
        public ActionResult<ProductDto> FindProduct(string productName)
        {
            return _productService.FindProduct(productName);
        }

        [HttpPut]
        public ActionResult<ProductDto> UpdateProduct([FromBody] UpsertProductArgs upsertProductArgs)
        {
            return _productConfigurationService.UpdateProduct(upsertProductArgs);
        }

        [Route("{productName}/markdown")]
        [HttpPut]
        public ActionResult<ProductDto> CreateMarkdown(string productName, [FromBody] UpsertProductMarkdownArgs upsertProductMarkdownArgs)
        {
            return _productMarkdownConfigurationService.UpsertProductMarkdown(upsertProductMarkdownArgs);
        }
    }
}
