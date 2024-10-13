namespace StoreStock.DataAccess.Entities
{
    // ProductEntity represents the product table in the database.
    public class ProductEntity
    {
        // Unique identifier for each product in the database.
        // This corresponds to the primary key in the table.
        public Guid Id { get; set; }

        // Name of the product, which can be set and retrieved.
        // Initialized with an empty string to avoid null values.
        public string Name { get; set; } = string.Empty;

        // Price of the product, represented as a decimal for accurate currency handling.
        public decimal Price { get; set; }

        // The stock quantity of the product, indicating how many units are available.
        public int StockQuantity { get; set; }
    }
}
