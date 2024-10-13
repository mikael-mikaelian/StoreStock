using Microsoft.EntityFrameworkCore;                  // Provides EF Core functionalities to interact with the database
using Microsoft.EntityFrameworkCore.Metadata.Builders; // Used to configure entity mappings
using StoreStock.DataAccess.Entities;                 // Imports the ProductEntity class
using StoreStock.Core.Models;                         // Imports the Product model (for constants like MAX_PRODUCT_NAME_LENGTH)
using System.ComponentModel.DataAnnotations;          // Used for data validation annotations (not used directly here)

namespace StoreStock.DataAccess.Configurations
{
    // ProductConfiguration class configures the ProductEntity mappings to the database.
    // Implements IEntityTypeConfiguration for strongly-typed configuration of ProductEntity.
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        // Configure method defines how the ProductEntity is mapped to the database schema.
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            // Sets the primary key for the ProductEntity (Id column).
            builder.HasKey(x => x.Id);

            // Configures the Name property to have a maximum length defined by the Product model's constant
            // and ensures that it is a required (non-nullable) field.
            builder.Property(p => p.Name)
                   .HasMaxLength(Product.MAX_PRODUCT_NAME_LENGTH)
                   .IsRequired();

            // Configures the Price property as required (non-nullable).
            builder.Property(p => p.Price)
                   .IsRequired();

            // Configures the StockQuantity property as required (non-nullable).
            builder.Property(p => p.StockQuantity)
                   .IsRequired();
        }
    }
}
