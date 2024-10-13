// Import necessary namespaces
using Microsoft.EntityFrameworkCore;                  // Provides functionality for interacting with a database using Entity Framework Core
using StoreStock.Application.Services;               // Imports services from the Application layer of the project
using StoreStock.Core.Abstractions;                  // Imports interface definitions or abstract classes for core functionality
using StoreStock.DataAccess;                         // Provides access to the DataAccess layer of the project
using StoreStock.DataAccess.Repositories;            // Imports repository classes for database operations

// Create a WebApplication builder to configure the web application
var builder = WebApplication.CreateBuilder(args);

// Add Authentication middleware to the service container
builder.Services.AddAuthentication();               // Configures authentication services for the app

// Add services to the container
builder.Services.AddControllers();                  // Adds controller support, enabling handling of HTTP requests
builder.Services.AddEndpointsApiExplorer();         // Adds support for API explorer, used for generating API documentation (like Swagger)

// Configure the ProductDBContext to use a PostgreSQL database with the connection string from the configuration
builder.Services.AddDbContext<ProductDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ProductDBContext)))
);

// Register services with the dependency injection container
builder.Services.AddScoped<IProductService, ProductService>();    // Registers ProductService with the scoped lifetime for IProductService
builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Registers ProductRepository with the scoped lifetime for IProductRepository

// Build the application
var app = builder.Build();

// Configure middleware

app.UseHttpsRedirection();    // Ensures the app redirects HTTP requests to HTTPS

app.UseAuthorization();       // Enables authorization middleware for controlling access to resources

app.MapControllers();         // Maps controller endpoints to handle HTTP requests

// Enable CORS (Cross-Origin Resource Sharing) to allow requests from a specific origin
app.UseCors(x =>
{
    // Allow any header and method, but restrict origin to "http://localhost:3000"
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http://localhost:3000");
    x.WithMethods().AllowAnyMethod();
});

// Run the application
app.Run();                    // Starts the application and begins listening for incoming HTTP requests
