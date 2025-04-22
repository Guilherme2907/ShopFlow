namespace GM.ShopFlow.Order.Domain.ValueObjects;

public class Email
{
    public string Value { get; private set; }

    public Email(string value)
    {
        Value = value;
    }
}
