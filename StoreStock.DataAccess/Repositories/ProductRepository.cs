using Microsoft.EntityFrameworkCore;            // Provides EF Core functionalities to interact with the database
using StoreStock.Core.Abstractions;             // Imports the IProductRepository interface
using StoreStock.Core.Models;                   // Imports the Product model for validation and creation
using StoreStock.DataAccess.Entities;           // Imports the ProductEntity class

namespace StoreStock.DataAccess.Repositories
{
    // The ProductRepository class implements the IProductRepository interface,
    // providing concrete implementations for data access operations related to products.
    public class ProductRepository : IProductRepository
    {
        // Field to hold the database context (ProductDBContext) to interact with the database.
        private readonly ProductDBContext _context;

        // Constructor that initializes the repository with a database context.
        public ProductRepository(ProductDBContext context)
        {
            _context = context;
        }

        // Retrieves all products from the database, maps them to the Product model, and returns a list.
        public async Task<List<Product>> Get()
        {
            // Fetches all product entities from the database without tracking changes (AsNoTracking for better performance).
            var productEntities = await _context.Products
                .AsNoTracking()
                .ToListAsync();

            // Maps each ProductEntity to a Product model and returns a list of Product objects.
            var products = productEntities
                .Select(p => Product.Create(p.Id, p.Name, p.Price, p.StockQuantity).Product)
                .ToList();

            return products;
        }

        // Creates a new product in the database.
        // Takes a Product model as input, converts it to ProductEntity, and adds it to the database.
        public async Task<Guid> Create(Product product)
        {
            // Maps the Product model to the ProductEntity used by EF Core for database operations.
            var productEntity = new ProductEntity
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };

            // Adds the ProductEntity to the database and saves the changes asynchronously.
            await _context.Products.AddAsync(productEntity);
            await _context.SaveChangesAsync();

            // Returns the GUID of the newly created product.
            return productEntity.Id;
        }

        // Updates an existing product in the database by its ID, setting new values for Name, Price, and StockQuantity.
        public async Task<Guid> Update(Guid id, string name, decimal price, int stockQuantity)
        {
            // Uses ExecuteUpdateAsync to directly update fields without fetching the entity first, improving performance.
            await _context.Products
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.Name, p => name)
                    .SetProperty(p => p.Price, p => price)
                    .SetProperty(p => p.StockQuantity, p => stockQuantity)
                );

            // Returns the GUID of the updated product.
            return id;
        }

        // Deletes a product from the database by its ID.
        public async Task<Guid> Delete(Guid id)
        {
            // Uses ExecuteDeleteAsync to delete the product directly by its ID.
            await _context.Products
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();

            // Returns the GUID of the deleted product.
            return id;
        }
    }
}
