using Microsoft.AspNetCore.Mvc;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;

namespace PillarTechnology.GroceryPointOfSale.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductConfigurationService _productConfigurationService;

        public ProductController(IProductConfigurationService productConfigurationService)
        {
            _productConfigurationService = productConfigurationService;
        }

        [HttpPost]
        public ActionResult<ProductDto> CreateProduct([FromBody] UpsertProductArgs upsertProductArgs)
        {
            return _productConfigurationService.CreateProduct(upsertProductArgs);
        }
    }
}
