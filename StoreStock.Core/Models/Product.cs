namespace StoreStock.Core.Models
{
    // The Product class represents a product in the store, containing properties like ID, Name, Price, and StockQuantity.
    public class Product
    {
        // Constant defining the maximum length for a product name.
        public const int MAX_PRODUCT_NAME_LENGTH = 50;

        // Private constructor to ensure controlled object creation via the static Create method.
        Product(Guid id, string name, decimal price, int stockQuantity)
        {
            // Initializes properties with values passed to the constructor.
            Id = id;
            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
        }

        // Property for the product ID, which is read-only (immutable after being set in the constructor).
        public Guid Id { get; }

        // Property for the product name, initialized with an empty string as a default value.
        public string Name { get; } = string.Empty;

        // Property for the product price, which is also read-only.
        public decimal Price { get; }

        // Property for the product's stock quantity (how many units are in stock).
        public int StockQuantity { get; }

        // Static factory method to create a Product object, applying validation logic and returning a tuple (Product, Error).
        public static (Product Product, string Error) Create(Guid id, string name, decimal price, int stockQuantity)
        {
            // String to store any validation errors.
            string error = string.Empty;

            // Validate the product name: it must not be empty or exceed the max length.
            if (name.Length > MAX_PRODUCT_NAME_LENGTH || string.IsNullOrEmpty(name))
            {
                error += "Name length has to be less than 50 and cannot be null.\n";  // Append error if validation fails.
            }

            // Validate that the price is non-negative.
            if (price < 0)
            {
                error += "Price cannot be negative.\n";  // Append error if validation fails.
            }

            // Validate that the stock quantity is non-negative.
            if (stockQuantity < 0)
            {
                error += "Stock Quantity cannot be negative.";  // Append error if validation fails.
            }

            // Create a new Product instance using the validated data.
            var product = new Product(id, name, price, stockQuantity);

            // Return a tuple containing the created Product object and any validation errors.
            return (product, error);
        }
    }
}
