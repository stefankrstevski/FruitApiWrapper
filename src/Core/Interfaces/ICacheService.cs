namespace Core.Interfaces;

public interface ICacheService<T> where T : class
{
    Task<T> GetCachedAsync(string key);
    Task SetCachedAsync(string key, T item);
}
