using Microsoft.AspNetCore.Mvc;                  // Provides attributes and classes to define ASP.NET Core MVC controllers and actions
using StoreStock.API.Contracts;                  // Imports the ProductRequest and ProductResponse contracts from the API layer
using StoreStock.Core.Abstractions;              // Imports service abstractions/interfaces from the Core layer
using StoreStock.Core.Models;                    // Imports core models like Product from the Core layer

namespace StoreStock.API.Controllers
{
    // This attribute marks this class as an API controller, meaning it will handle HTTP requests.
    [ApiController]
    // Route attribute specifies the base URL pattern for the controller, e.g., "/products".
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        // Dependency injection for IProductService. This service will handle business logic related to products.
        public IProductService _productService;

        // Constructor for the controller where IProductService is injected.
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // HTTP GET method to retrieve all products. Returns a list of ProductResponse objects.
        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetAllBooks()
        {
            // Call the service to retrieve all products.
            var products = await _productService.GetAllProducts();
            
            // Map the products to ProductResponse objects to send in the response.
            var response = products.Select(p => new ProductResponse(p.Id, p.Name, p.Price, p.StockQuantity));
            
            // Return the result with a 200 OK status code.
            return Ok(response);
        }

        // HTTP PUT method to update a product. Requires the product ID and the updated product data from the request body.
        [HttpPut("{id:guid}")]   // The route specifies the product ID must be a GUID in the URL.
        public async Task<ActionResult<Guid>> UpdateProduct(Guid id, [FromBody] ProductRequest request)
        {
            // Call the service to update the product and return the updated product ID with a 200 OK status.
            return Ok(await _productService.UpdateProduct(id, request.name, request.price, request.stockQuantity));
        }

        // HTTP POST method to add a new product. Requires the product data from the request body.
        [HttpPost]
        public async Task<ActionResult<Guid>> AddProduct([FromBody] ProductRequest request)
        {
            // Create a new product object, ensuring validation logic is applied (via Product.Create).
            var (product, error) = Product.Create (
                Guid.NewGuid(),                    // Generate a new GUID for the product ID.
                request.name,                      // Extract the product name from the request.
                request.price,                     // Extract the product price from the request.
                request.stockQuantity              // Extract the stock quantity from the request.
            );

            // If there is an error (e.g., validation failure), return a BadRequest response with the error message.
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            // Call the service to add the new product and return the new product ID with a 200 OK status.
            return Ok(await _productService.AddProduct(product));
        }

        // HTTP DELETE method to delete a product by its ID (GUID).
        [HttpDelete("{id:guid}")]   // The route specifies the product ID must be a GUID in the URL.
        public async Task<ActionResult<Guid>> DeleteProduct(Guid id)
        {
            // Call the service to delete the product by its ID and return the deleted product ID with a 200 OK status.
            return Ok(await _productService.DeleteProduct(id));
        }
    }
}
