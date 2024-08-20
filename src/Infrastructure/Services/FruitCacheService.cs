using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Services;

public class FruitCacheService : IFruitCacheService
{
    private readonly ICacheService<Fruit> _cacheService;

    public FruitCacheService(ICacheService<Fruit> cacheService)
    {
        _cacheService = cacheService;
    }

    public Task<Fruit> GetCachedFruitAsync(string name) => _cacheService.GetCachedAsync(name);
    public Task SetCachedFruitAsync(string name, Fruit fruit) => _cacheService.SetCachedAsync(name, fruit);
}