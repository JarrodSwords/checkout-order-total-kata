using Microsoft.AspNetCore.Mvc;
using PointOfSale.Services;

namespace PointOfSale.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Dependencies

        private readonly IProductConfigurationService _productConfigurationService;
        private readonly IProductMarkdownConfigurationService _productMarkdownConfigurationService;
        private readonly IProductService _productService;
        private readonly IProductSpecialConfigurationService _productSpecialConfigurationService;

        public ProductController(
            IProductConfigurationService productConfigurationService,
            IProductMarkdownConfigurationService productMarkdownConfigurationService,
            IProductService productService,
            IProductSpecialConfigurationService productSpecialConfigurationService
        )
        {
            _productConfigurationService = productConfigurationService;
            _productMarkdownConfigurationService = productMarkdownConfigurationService;
            _productService = productService;
            _productSpecialConfigurationService = productSpecialConfigurationService;
        }

        #endregion Dependencies

        [HttpPost]
        public ActionResult<ProductDto> CreateProduct([FromBody] UpsertProductArgs args)
        {
            return _productConfigurationService.CreateProduct(args);
        }

        [Route("{productName}")]
        [HttpGet]
        public ActionResult<ProductDto> FindProduct(string productName)
        {
            return _productService.FindProduct(productName);
        }

        [Route("{productName}")]
        [HttpPut]
        public ActionResult<ProductDto> UpdateProduct(string productName, [FromBody] UpsertProductArgs args)
        {
            args.ProductName = productName;
            return _productConfigurationService.UpdateProduct(args);
        }

        [Route("{productName}/markdown")]
        [HttpPut]
        public ActionResult<ProductDto> CreateMarkdown(string productName, [FromBody] UpsertProductMarkdownArgs args)
        {
            args.ProductName = productName;
            return _productMarkdownConfigurationService.UpsertProductMarkdown(args);
        }

        [Route("{productName}/special")]
        [HttpPut]
        public ActionResult<ProductDto> CreateSpecial(string productName, [FromBody] CreateSpecialArgs args)
        {
            args.ProductName = productName;
            return _productSpecialConfigurationService.CreateSpecial(args);
        }
    }
}
