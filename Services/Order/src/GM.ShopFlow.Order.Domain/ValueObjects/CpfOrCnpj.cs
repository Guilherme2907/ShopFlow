namespace GM.ShopFlow.Order.Domain.ValueObjects;

public class CpfOrCnpj
{
    public string Value { get; private set; }

    public CpfOrCnpj(string value)
    {
        Value = value;
    }
}
