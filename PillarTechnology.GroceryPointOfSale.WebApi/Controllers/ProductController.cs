using Microsoft.AspNetCore.Mvc;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;

namespace PillarTechnology.GroceryPointOfSale.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Dependencies

        private readonly BuyNForXAmountConfigurationService _buyNForXAmountOffConfigurationService;
        private readonly BuyNGetMAtXPercentOffConfigurationService _buyNGetMAtXPercentOffConfigurationService;
        private readonly BuyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService _buyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService;
        private readonly IProductConfigurationService _productConfigurationService;
        private readonly IProductMarkdownConfigurationService _productMarkdownConfigurationService;
        private readonly IProductService _productService;

        public ProductController(
            BuyNForXAmountConfigurationService buyNForXAmountOffConfigurationService,
            BuyNGetMAtXPercentOffConfigurationService buyNGetMAtXPercentOffConfigurationService,
            BuyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService buyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService,
            IProductConfigurationService productConfigurationService,
            IProductMarkdownConfigurationService productMarkdownConfigurationService,
            IProductService productService
        )
        {
            _buyNForXAmountOffConfigurationService = buyNForXAmountOffConfigurationService;
            _buyNGetMAtXPercentOffConfigurationService = buyNGetMAtXPercentOffConfigurationService;
            _buyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService = buyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService;
            _productConfigurationService = productConfigurationService;
            _productMarkdownConfigurationService = productMarkdownConfigurationService;
            _productService = productService;
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
            args.Name = productName;
            return _productConfigurationService.UpdateProduct(args);
        }

        [Route("{productName}/markdown")]
        [HttpPut]
        public ActionResult<ProductDto> CreateMarkdown(string productName, [FromBody] UpsertProductMarkdownArgs args)
        {
            args.ProductName = productName;
            return _productMarkdownConfigurationService.UpsertProductMarkdown(args);
        }

        [Route("{productName}/buyNForXAmountSpecial")]
        [HttpPut]
        public ActionResult<ProductDto> CreateBuyNForXAmountSpecial(string productName, [FromBody] CreateBuyNForXAmountSpecialArgs args)
        {
            args.ProductName = productName;
            return _buyNForXAmountOffConfigurationService.CreateSpecial(args);
        }

        [Route("{productName}/buyNGetMAtXPercentOffSpecial")]
        [HttpPut]
        public ActionResult<ProductDto> CreateBuyNGetMAtXPercentOffSpecial(string productName, [FromBody] CreateBuyNGetMAtXPercentOffSpecialArgs args)
        {
            args.ProductName = productName;
            return _buyNGetMAtXPercentOffConfigurationService.CreateSpecial(args);
        }

        [Route("{productName}/buyNGetMOfEqualOrLesserValueAtXPercentOffSpecial")]
        [HttpPut]
        public ActionResult<ProductDto> CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(string productName, [FromBody] CreateBuyNGetMAtXPercentOffSpecialArgs args)
        {
            args.ProductName = productName;
            return _buyNGetMOfEqualOrLesserValueAtXPercentOffConfigurationService.CreateSpecial(args);
        }
    }
}
