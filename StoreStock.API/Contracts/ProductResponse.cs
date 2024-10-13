namespace StoreStock.API.Contracts;

public record ProductResponse
(
    Guid id,
    string name,
    decimal price,
    int stockQuantity
);
