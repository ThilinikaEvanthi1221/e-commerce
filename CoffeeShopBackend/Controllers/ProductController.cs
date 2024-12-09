using Microsoft.AspNetCore.Mvc;
using CoffeeShopBackend.Services;
using MongoDB.Bson;
using CoffeeShopBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // GET api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            // Convert string to ObjectId
            var objectId = new ObjectId(id);
            var product = await _productService.GetProductByIdAsync(objectId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // Other methods (POST, PUT, DELETE)...
    }
}
