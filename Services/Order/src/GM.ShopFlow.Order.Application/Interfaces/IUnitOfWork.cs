namespace GM.ShopFlow.Order.Application.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
}
