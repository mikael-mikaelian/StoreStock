using StoreStock.Core.Abstractions;             // Imports service and repository abstractions (interfaces)
using StoreStock.Core.Models;                   // Imports the Product model class

namespace StoreStock.Application.Services
{
    // The ProductService class implements the IProductService interface.
    // It acts as the service layer handling business logic related to products.
    public class ProductService : IProductService
    {  
        // Injects the product repository to interact with the data layer.
        // 'readonly' ensures the repository cannot be reassigned after construction.
        public readonly IProductRepository _productRepository;

        // Constructor where the IProductRepository is injected.
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;  // Assigns the injected repository to the local field.
        }

        // Method to retrieve all products by calling the repository's Get method.
        public Task<List<Product>> GetAllProducts()
        {
            return _productRepository.Get();  // Delegates the retrieval of all products to the repository.
        }

        // Method to update an existing product by passing product details to the repository.
        public Task<Guid> UpdateProduct(Guid id, string name, decimal price, int stockQuantity)
        {
            // Calls the repository's Update method, passing the product ID and updated data.
            return _productRepository.Update(id, name, price, stockQuantity);
        }

        // Method to add a new product by passing the Product object to the repository's Create method.
        public Task<Guid> AddProduct(Product product)
        {
            // Calls the repository's Create method to add the product to the database.
            return _productRepository.Create(product);
        }

        // Method to delete a product by passing the product ID to the repository's Delete method.
        public Task<Guid> DeleteProduct(Guid id)
        {
            // Calls the repository's Delete method to remove the product by its ID.
            return _productRepository.Delete(id);
        }
    }
}
