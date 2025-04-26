namespace GM.ShopFlow.Order.Application.UseCases.Order.CreateOrder;

public record CreateOrderItemInput(
    int Quantity,
    Guid ProductId,
    string ProductName,
    decimal ProductPrice
);

