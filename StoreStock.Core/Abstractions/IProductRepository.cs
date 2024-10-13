using StoreStock.Core.Models;                  // Imports the Product model

namespace StoreStock.Core.Abstractions
{
    // The IProductRepository interface defines the contract for interacting with product data.
    // Any class implementing this interface must provide implementations for these methods.

    public interface IProductRepository
    {
        // Method to create a new product. 
        // Accepts a Product object and returns the GUID of the newly created product.
        Task<Guid> Create(Product product);

        // Method to retrieve all products.
        // Returns a list of Product objects.
        Task<List<Product>> Get();

        // Method to delete a product by its ID (GUID).
        // Returns the GUID of the deleted product.
        Task<Guid> Delete(Guid id);

        // Method to update an existing product.
        // Accepts the product's ID and new details (name, price, stock quantity).
        // Returns the GUID of the updated product.
        Task<Guid> Update(Guid id, string name, decimal price, int stockQuantity);
    }
}
