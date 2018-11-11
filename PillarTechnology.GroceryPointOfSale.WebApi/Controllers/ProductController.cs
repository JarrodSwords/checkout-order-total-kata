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

        [HttpPut]
        public ActionResult<ProductDto> UpdateProduct([FromBody] UpsertProductArgs args)
        {
            return _productConfigurationService.UpdateProduct(args);
        }

        [Route("{productName}/markdown")]
        [HttpPut]
        public ActionResult<ProductDto> CreateMarkdown(string productName, [FromBody] UpsertProductMarkdownArgs args)
        {
            return _productMarkdownConfigurationService.UpsertProductMarkdown(args);
        }

        [Route("{productName}/buyNForXAmountSpecial")]
        [HttpPut]
        public ActionResult<ProductDto> CreateBuyNForXAmountSpecial(string productName, [FromBody] CreateBuyNForXAmountSpecialArgs args)
        {
            return _productSpecialConfigurationService.CreateBuyNForXAmountSpecial(args);
        }

        [Route("{productName}/buyNGetMAtXPercentOffSpecial")]
        [HttpPut]
        public ActionResult<ProductDto> CreateBuyNGetMAtXPercentOffSpecial(string productName, [FromBody] CreateBuyNGetMAtXPercentOffSpecialArgs args)
        {
            return _productSpecialConfigurationService.CreateBuyNGetMAtXPercentOffSpecial(args);
        }

        [Route("{productName}/buyNGetMOfEqualOrLesserValueAtXPercentOffSpecial")]
        [HttpPut]
        public ActionResult<ProductDto> CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(string productName, [FromBody] CreateBuyNGetMAtXPercentOffSpecialArgs args)
        {
            return _productSpecialConfigurationService.CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(args);
        }
    }
}
