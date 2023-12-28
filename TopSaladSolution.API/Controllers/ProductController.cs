using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopSaladSolution.Interface;
using TopSaladSolution.Model.Products;
using TopSaladSolution.Service;

namespace TopSaladSolution.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(Name = "GetAllProducts")]
        public async Task <IActionResult> GetAll()
        {
            try
            {
                return Ok(await _productService.GetAllAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _productService.GetById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateRequest product)
        {
            try
            {
                var newProduct = await _productService.Create(product);
                return Ok(newProduct);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductEditRequest product)
        {
            try
            {
                var updatedProduct = await _productService.Update(product);
                return Ok(updatedProduct);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<IActionResult> DeleteProduct(ProductSoftDeleteRequest product)
        {
            try
            {
                var deletedProduct = await _productService.SoftDelete(product);
                return Ok(deletedProduct);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
