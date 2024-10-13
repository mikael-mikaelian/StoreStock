using StoreStock.Core.Models;                  // Imports the Product model

namespace StoreStock.Core.Abstractions
{
    // The IProductService interface defines the contract for product-related business logic.
    // Any class implementing this interface must provide implementations for these methods.

    public interface IProductService
    {
        // Method to add a new product.
        // Accepts a Product object and returns the GUID of the newly added product.
        Task<Guid> AddProduct(Product product);

        // Method to retrieve all products.
        // Returns a list of Product objects.
        Task<List<Product>> GetAllProducts();

        // Method to delete a product by its ID (GUID).
        // Returns the GUID of the deleted product.
        Task<Guid> DeleteProduct(Guid id);

        // Method to update an existing product.
        // Accepts the product's ID and new details (name, price, stock quantity).
        // Returns the GUID of the updated product.
        Task<Guid> UpdateProduct(Guid id, string name, decimal price, int stockQuantity);
    }
}
