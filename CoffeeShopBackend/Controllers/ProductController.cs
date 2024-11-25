using CoffeeShopBackend.Models;
using CoffeeShopBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShopBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

       
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await _productService.GetAsync(); 
            return Ok(products);
        }

                [HttpPost]
        public async Task<ActionResult> Create([FromBody] Product product)
        {
            await _productService.CreateAsync(product); 
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product); 
        }
    }
}
