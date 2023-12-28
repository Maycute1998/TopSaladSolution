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

        [HttpGet(Name = "GetProductById")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _productService.GetById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost(Name = "AddProduct")]
        public async Task<IActionResult> CreateProduct(ProductCreateRequest product)
        {
            try
            {
                var book = await _productService.Create(product);
                return Ok(book);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
