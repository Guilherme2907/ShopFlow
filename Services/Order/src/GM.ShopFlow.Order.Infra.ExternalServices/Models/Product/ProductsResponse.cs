namespace GM.ShopFlow.Order.Infra.ExternalServices.Models.Product;

public record ProductsResponse(
    Guid Id,
    string Name,
    decimal Price,
    int Quantity
);

