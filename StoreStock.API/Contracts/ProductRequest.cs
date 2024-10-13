namespace StoreStock.API.Contracts;

public record ProductRequest
(
    string name,
    decimal price,
    int stockQuantity
);
