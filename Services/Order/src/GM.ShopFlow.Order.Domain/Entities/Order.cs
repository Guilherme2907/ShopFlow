using GM.ShopFlow.Order.Domain.Enums;
using GM.ShopFlow.Order.Domain.SeedWork;
using GM.ShopFlow.Order.Domain.States.Order;

namespace GM.ShopFlow.Order.Domain.Entities;

public class Order : Entity
{
    public OrderStatus Status
    {
        get => _state.Status;
        private set;
    }

    private OrderState _state;

    public float PercentageDiscount { get; private set; }

    public decimal Total { get; private set; }

    private readonly IList<OrderItem> _items = [];

    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public Customer Customer { get; set; }

    public Guid CustomerId { get; set; }

    public Order(IList<OrderItem> items, Customer customer, float percentageDiscount = 0)
    {
        _state = new CreatedOrderState();
        PercentageDiscount = percentageDiscount;
        _items = items;
        Customer = customer;
        Total = CalculateTotal();

        Validate();
    }

    private Order() { }

    private void Validate()
    {
        if (!_items.Any())
            throw new InvalidDataException("The order must have at least 1 item");

        if(Customer is null)
            throw new InvalidDataException("The order must have a valid customer");

        if (Total <= 0)
            throw new InvalidDataException("Total must be a valid value");
    }

    private decimal CalculateTotal()
    {
        var partialTotal = Items.Sum(i => i.Total);

        var discountValue = partialTotal * (decimal)PercentageDiscount / 100;

        return partialTotal - discountValue;
    }

    public void AddItem(OrderItem item)
    {
        _items.Add(item);
    }

    public void ChagePercentageDiscount(float percentageDiscount)
    {
        PercentageDiscount = percentageDiscount;
    }

    public void Create()
    {
        _state.Create(this);
    }

    public void Cancel()
    {
        _state.Cancel(this);
    }

    public void Send()
    {
        _state.Send(this);
    }

    public void Finish()
    {
        _state.Finish(this);
    }

    internal void TransitionTo(OrderState state)
    {
        _state = state;
    }
}
