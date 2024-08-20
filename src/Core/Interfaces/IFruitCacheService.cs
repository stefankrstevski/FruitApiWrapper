using Core.Entities;

namespace Core.Interfaces;

public interface IFruitCacheService
{
    Task<Fruit> GetCachedFruitAsync(string name);
    Task SetCachedFruitAsync(string name, Fruit fruit);
}
