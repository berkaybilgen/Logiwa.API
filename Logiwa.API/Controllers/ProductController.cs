using Logiwa.Domain.Models;
using Logiwa.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Logiwa.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _productService.GetProducts();

            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _productService.GetById(id);

            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel product)
        {
            var result = await _productService.Create(product);

            return new JsonResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductModel product)
        {
            var result = await _productService.Update(product);

            return new JsonResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _productService.Delete(productId);

            return new JsonResult(result);
        }

        [HttpGet("{keyword}")]
        public async Task<IActionResult> FilterByKeyword([FromRoute] string keyword)
        {
            var result = await _productService.FilterByKeyword(keyword);

            return new JsonResult(result);
        }

        [HttpGet("{minValue}/{maxValue}")]
        public async Task<IActionResult> FilterStockQuantityRange([FromRoute] int minValue, [FromRoute] int maxValue)
        {
            var result = await _productService.FilterStockQuantityRange(minValue, maxValue);

            return new JsonResult(result);
        }
    }
}
