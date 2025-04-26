namespace GM.ShopFlow.Product.Application.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
}
