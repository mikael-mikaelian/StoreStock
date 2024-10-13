using Microsoft.EntityFrameworkCore;                   // Provides EF Core functionalities to interact with the database
using StoreStock.DataAccess.Entities;                  // Imports the ProductEntity class

namespace StoreStock.DataAccess
{
    // ProductDBContext represents the database context for the application.
    // It inherits from DbContext, enabling interaction with the database using Entity Framework Core.
    public class ProductDBContext : DbContext
    {
        // Constructor that accepts DbContextOptions and passes them to the base DbContext constructor.
        // This allows the context to be configured with options like connection strings and behaviors.
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options) {}

        // DbSet representing the collection of ProductEntity objects in the database.
        // This property will be used for querying and saving instances of ProductEntity.
        public DbSet<ProductEntity> Products { get; set; }
    }
}
