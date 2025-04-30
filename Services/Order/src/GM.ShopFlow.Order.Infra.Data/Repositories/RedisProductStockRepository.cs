using GM.ShopFlow.Order.Application.Interfaces;
using GM.ShopFlow.Order.Application.Models;
using StackExchange.Redis;

namespace GM.ShopFlow.Order.Infra.Data.Repositories;

public class RedisProductStockRepository(IConnectionMultiplexer redisConnection) : IProductStockRepository
{
    private readonly IDatabase _redisDb = redisConnection.GetDatabase();
    private const string CachePrefix = "product_stock";

    public async Task SaveAsync(IEnumerable<ProductStock> productStocks, CancellationToken cancellationToken)
    {
        var stocks = productStocks.Select(s => new KeyValuePair<RedisKey, RedisValue>(GetKey(s.ProductId), s.Quantity));

        await _redisDb.StringSetAsync(stocks.ToArray());

        await _redisDb.StringSetAsync(CachePrefix, true);
    }

    public async Task SetStockAsync(string productId, int newQuantity, CancellationToken cancellationToken)
    {
        await _redisDb.StringSetAsync(GetKey(productId), newQuantity);
    }

    public async Task<int?> GetStockAsync(string productId, CancellationToken cancellationToken)
    {
        var value = await _redisDb.StringGetAsync(GetKey(productId));
        return value.HasValue ? (int?)value : null;
    }
        
    
    public async Task<bool> HasProductStocksAsync(CancellationToken cancellationToken)
    {
        var value = await _redisDb.StringGetAsync(CachePrefix);

        return value.HasValue && (bool)value;
    }

    private static string GetKey(string key)
    {
        return $"{CachePrefix}:{key}";
    }
}
