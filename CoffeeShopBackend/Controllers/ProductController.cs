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

         // POST api/products
        [HttpPost]
public async Task<IActionResult> CreateProduct([FromBody] Product newProduct)
{
    if (newProduct == null)
    {
        return BadRequest("Product data is required.");
    }

    if (newProduct.Id == ObjectId.Empty)
    {
        newProduct.Id = ObjectId.GenerateNewId(); // Explicitly set the Id
    }

    // Save the product
    await _productService.CreateProductAsync(newProduct);

    // Debugging: Log the product Id
    Console.WriteLine($"Product Created: Id = {newProduct.Id}");

    // Ensure the route name and parameter match
    return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id.ToString() }, newProduct);
}

        // GET api/products/{id}
        [HttpGet("products/{id}")]
public async Task<IActionResult> GetProductById(string id)
{
    if (!ObjectId.TryParse(id, out var objectId))
    {
        return BadRequest("Invalid Id format.");
    }

    var product = await _productService.GetProductByIdAsync(objectId);

    if (product == null)
    {
        return NotFound();
    }

    return Ok(product);
}

       

        // PUT api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Product updatedProduct)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid product ID format.");
            }

            // Retrieve the product to ensure it exists before updating
            var existingProduct = await _productService.GetProductByIdAsync(objectId);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            // Update the product
            updatedProduct.Id = objectId; // Ensure the ID remains the same
            await _productService.UpdateProductAsync(objectId, updatedProduct);

            return Ok($"Product with ID {id} updated successfully.");
        }

        // DELETE api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid product ID format.");
            }

            var existingProduct = await _productService.GetProductByIdAsync(objectId);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            await _productService.DeleteProductAsync(objectId);
            return Ok($"Product with ID {id} deleted successfully.");
        }
    }
}
