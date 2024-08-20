using Core.Entities;

namespace Core.Interfaces;

public interface IFruitService
{
    Task<FruitWithMetadata> GetFruitWithMetadataAsync(string name);
    Task<bool> AddOrUpdateMetadataAsync(string name, Dictionary<string, string> metadata);
    Task<bool> RemoveMetadataAsync(string name);
}
