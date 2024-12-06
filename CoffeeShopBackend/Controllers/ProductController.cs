using CoffeeShopBackend.Models;
using CoffeeShopBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        // Constructor for dependency injection
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // GET api/products - Fetch all products
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await _productService.GetProductsAsync(); 
            return Ok(products); // Returns the list of products as JSON
        }

        // POST api/products - Create a new product
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Product product)
        {
            await _productService.AddProductAsync(product); // Use AddProductAsync instead of Create
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product); 
        }
    }
}
